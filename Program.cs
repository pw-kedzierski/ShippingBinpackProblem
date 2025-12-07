using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BinPackingApp.Core.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(services =>
{
    var baseAddress = builder.HostEnvironment.IsDevelopment() 
        ? "http://localhost:5052/"
        : "https://esa.instech.no/";
    
    return new HttpClient
    {
        BaseAddress = new Uri(baseAddress),
        Timeout = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddScoped<IFleetApiService, FleetApiService>();
builder.Services.AddScoped<IBinPackService, BinPackService>();

var app = builder.Build();
await app.RunAsync();
