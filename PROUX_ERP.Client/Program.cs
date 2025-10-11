using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PROUX_ERP.Client;
using PROUX_ERP.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient federado con cookies (Blazor WASM ya usa el handler del navegador)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7057/")
});

builder.Services.AddScoped<UserService>();

await builder.Build().RunAsync();
