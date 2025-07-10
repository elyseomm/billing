using BlazorApp2;
using BlazorApp2.API;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration.Memory;
using static System.Net.WebRequestMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var restApi = builder.Configuration.GetValue<string>("restApi") ?? throw new Exception("Unable to read restApi URL..");

//var memData = new Dictionary<string, string?>()
//{
//    { "restApi", restApi },
//};
//var memoryConfig = new MemoryConfigurationSource { InitialData = memData };
//builder.Configuration.Add(memoryConfig);

builder.Services.AddScoped( client => new CustomerApiClient { BaseAddress = new Uri(restApi!) } );
builder.Services.AddScoped(client => new ProductApiClient { BaseAddress = new Uri(restApi!) } );

await builder.Build().RunAsync();
