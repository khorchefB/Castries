namespace AuctionService.Extensions;

public static class AddMassTransit
{
    public static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration,
                                                    Assembly assembly = null)
    {
        services.AddMassTransit(config =>
       {

           if (assembly is not null)
           {
               config.AddConsumers(assembly);
               Console.WriteLine("assembly.GetName().Name == " + assembly.GetName().Name);
               config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(assembly.GetName().Name, false));
           }

           config.UsingRabbitMq((context, configRabbit) =>
          {
             configRabbit.ReceiveEndpoint($"{assembly.GetName().Name}-auction-created", e =>
             {
                 e.UseMessageRetry(r => r.Interval(5, 5));
                 e.ConfigureConsumer<AuctionCreatedConsumer>(context);
             });
             configRabbit.Host(new Uri(configuration["RabbitMq:Host"]!), host =>
             {
                 host.Username(configuration["RabbitMq:UserName"]);
                 host.Password(configuration["RabbitMq:Password"]);
             });
             configRabbit.ConfigureEndpoints(context);
         });
       });
    }
}
