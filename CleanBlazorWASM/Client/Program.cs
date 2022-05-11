global using CleanBlazorWASM.Client.Services.SuperHeroService;
global using CleanBlazorWASM.Shared;
using CleanBlazorWASM.Client;
using CleanBlazorWASM.Client.Services.MovieApiService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ISuperHeroService, SuperHeroService>();
builder.Services.AddScoped<IMovieApiService, MovieApiService>();

await builder.Build().RunAsync();
