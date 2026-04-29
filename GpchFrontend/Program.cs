using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GpchFrontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cliente para archivos estáticos (wwwroot)
builder.Services.AddHttpClient("Static", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

// URL base de la API (Azure Functions)
var apiBaseUrl = builder.HostEnvironment.IsDevelopment()
    ? "http://localhost:7071/api/"
    : "https://gpch-web.azurewebsites.net/api/";

// Cliente para la API
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Otros servicios
builder.Services.AddSingleton<GpchFrontend.Services.ThemeService>();

await builder.Build().RunAsync();
