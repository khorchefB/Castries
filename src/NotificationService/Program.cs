using System.Reflection;
using NotificationService.Extensions;
using NotificationService.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransitConfiguration(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddSignalR();
var app = builder.Build();
app.MapHub<NotificationHub>("/notifications");
 
app.Run();
