
namespace AuctionService.Extensions;

public static class AddMassTransit
{
    public static void  AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration, Assembly assembly){
       services.AddMassTransit(config =>
      {
          config.AddEntityFrameworkOutbox<AuctionDbContext>(outboxConfig => {
                outboxConfig.QueryDelay = TimeSpan.FromSeconds(10);
                outboxConfig.UsePostgres();
                outboxConfig.UseBusOutbox();
          });
          config.AddConsumers(assembly);
          config.UsingRabbitMq((context, configurator) =>
          {
              configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
              {
                  host.Username(configuration["MessageBroker:UserName"]);
                  host.Password(configuration["MessageBroker:Password"]);
              });
              configurator.ConfigureEndpoints(context);
          });
      });
    }
}
