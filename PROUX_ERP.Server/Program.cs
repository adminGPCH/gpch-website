// D:\GPCH\APP\PROUX_ERP.Server\Program.cs
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI (opcional en dev)
builder.Services.AddOpenApi();

// Autenticación: Cookie + OpenIdConnect (OIDC)
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        // Cookie de sesión del backend
        options.Cookie.Name = ".AspNetCore.Cookies";

        // Imprescindible para flujos OIDC cross-site
        options.Cookie.SameSite = SameSiteMode.None;

        // Solo en HTTPS
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        // Persistencia para login sigiloso
        options.ExpireTimeSpan = TimeSpan.FromHours(8);   // duración deseada de la sesión
        options.SlidingExpiration = true;                 // renueva la expiración con actividad

        // Evitar redirecciones automáticas en APIs: devolver 401/403
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api"))
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                }
                ctx.Response.Redirect(ctx.RedirectUri);
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api"))
                {
                    ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
                ctx.Response.Redirect(ctx.RedirectUri);
                return Task.CompletedTask;
            }
        };
    })
    .AddOpenIdConnect(options =>
    {
        options.Authority = builder.Configuration["Authentication:Authority"];
        options.ClientId = builder.Configuration["Authentication:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:ClientSecret"];

        options.ResponseType = "code";
        options.SaveTokens = true;

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");

        // Asegura HTTPS en el flujo OIDC
        options.RequireHttpsMetadata = true;

        // Ruta de callback por defecto: /signin-oidc
        // options.CallbackPath = "/signin-oidc";
    });

// Autorización
builder.Services.AddAuthorization();

// Controladores
builder.Services.AddControllers();

// CORS para el Client (https://localhost:7081)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7081")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Importante: HTTPS antes que cualquier emisión de cookie
app.UseHttpsRedirection();

// CORS
app.UseCors();

// Autenticación y autorización en orden
app.UseAuthentication();
app.UseAuthorization();

// Controladores
app.MapControllers();

app.Run();
