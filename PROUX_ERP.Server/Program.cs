// D:\GPCH\APP\PROUX_ERP.Server\Program.cs
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI (opcional en dev)
builder.Services.AddOpenApi();

// Autenticaci�n: Cookie + OpenIdConnect (OIDC)
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        // Cookie de sesi�n del backend
        options.Cookie.Name = ".AspNetCore.Cookies";

        // Imprescindible para flujos OIDC cross-site
        options.Cookie.SameSite = SameSiteMode.None;

        // Solo en HTTPS
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        // Persistencia para login sigiloso
        options.ExpireTimeSpan = TimeSpan.FromHours(8);   // duraci�n deseada de la sesi�n
        options.SlidingExpiration = true;                 // renueva la expiraci�n con actividad

        // Evitar redirecciones autom�ticas en APIs: devolver 401/403
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

// Autorizaci�n
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

// Importante: HTTPS antes que cualquier emisi�n de cookie
app.UseHttpsRedirection();

// CORS
app.UseCors();

// Autenticaci�n y autorizaci�n en orden
app.UseAuthentication();
app.UseAuthorization();

// Controladores
app.MapControllers();

app.Run();
