
using AuctionService.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMassTransitConfiguration(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.Authority = builder.Configuration["IdentityServiceUrl"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.NameClaimType = "username";
                });
builder.Services.AddGrpc();

// Configure the HTTP request pipeline.
 var app = builder.Build();
app.MapGrpcService<GrpcAuctionService>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


try{
    await app.InitiDb();
}catch  (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
