var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransitConfiguration(builder.Configuration, Assembly.GetExecutingAssembly());

// Configure the HTTP request pipeline.
 var app = builder.Build();
app.UseAuthorization();

app.MapControllers();


try{
    await app.InitiDb();
}catch  (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
