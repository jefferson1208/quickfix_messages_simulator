using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using quickfix_messages_simulator_core.Interfaces;
using quickfix_messages_simulator_core.Setup;
using quickfix_messages_simulator_home_broker.Components;
using quickfix_messages_simulator_interface.Setup;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var quickfixSocket = new QuickfixSocket();
quickfixSocket.Configure();

builder.Services.AddSingleton<ISocket>(quickfixSocket);
builder.Services.AddSingleton<MessageReceiver>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
