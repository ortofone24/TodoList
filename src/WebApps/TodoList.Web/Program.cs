using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using TodoList.Web;
using TodoList.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7034/") });
builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddSingleton<HubConnection>(sp =>
{
    return new HubConnectionBuilder()
        .WithUrl(new Uri("https://localhost:7034/notificationHub")) 
        .WithAutomaticReconnect()
        .Build();
});

await builder.Build().RunAsync();
