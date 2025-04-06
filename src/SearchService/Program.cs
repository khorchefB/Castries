
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSpecificHttpClient() ;
builder.Services.AddMassTransitConfiguration(builder.Configuration,Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.Lifetime.ApplicationStarted.Register(async () => {
    await app.InitDb();
});

app.Run();