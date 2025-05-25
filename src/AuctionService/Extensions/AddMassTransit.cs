
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
            var uri = configuration["RabbitMq:Host"];
            Console.WriteLine($"RabbitMQ Host: {uri}");
              configurator.Host(new Uri(configuration["RabbitMq:Host"]!), host =>
              {
                  host.Username(configuration["RabbitMq:UserName"]);
                  host.Password(configuration["RabbitMq:Password"]);
              });
              configurator.ConfigureEndpoints(context);
          });
      });
    }
}
