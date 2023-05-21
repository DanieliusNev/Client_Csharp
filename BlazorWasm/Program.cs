using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasm;
using BlazorWasm.Service;
using BlazorWasm.Services;
using BlazorWasm.Services.Http;

/*var builder = WebAssemblyHostBuilder.CreateDefault(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<DemoService>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:8080/")
});

await builder.Build().RunAsync();*/
var builder = WebAssemblyHostBuilder.CreateDefault(args);
var apiUrl = "http://localhost:8080/"; // Update with your API URL

builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<DemoService>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiUrl)
});

// Enable CORS
builder.Services.AddTransient(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri(apiUrl)
    };
    httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
    httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
    httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
    return httpClient;
});

await builder.Build().RunAsync();

