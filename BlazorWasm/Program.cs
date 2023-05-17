using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasm;
using BlazorWasm.Service;
using BlazorWasm.Services;
using BlazorWasm.Services.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();

/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });#2#*/

builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri("http://localhost:8080/") });


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5291")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:5291",
                "http://localhost:8080");
        });
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});


await builder.Build().RunAsync();


