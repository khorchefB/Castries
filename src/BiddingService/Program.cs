using AuctionService.Extensions;
using BiddingService.Data;
using BiddingService.Services;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<GrpcAuctionClient>();
builder.Services.AddHostedService<CheckAuctionFinished>();
builder.Services.AddMassTransitConfiguration(builder.Configuration, typeof(BiddingService.Consumers.AuctionCreatedConsumer).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.Authority = builder.Configuration["IdentityServiceUrl"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.NameClaimType = "username";
                });
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
try{
    await app.InitDb();
}catch  (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
 