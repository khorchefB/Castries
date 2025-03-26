using SearchService.Data;
using SearchService.Services;
using Microsoft.Extensions.DependencyInjection;
using SearchService.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSpecificHttpClient() ;
var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapControllers();
app.Lifetime.ApplicationStarted.Register(async () => {
    await app.InitDb();
});

app.Run();