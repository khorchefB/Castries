using System.Reflection;
using MassTransit;

namespace NotificationService.Extensions;

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
               config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(assembly.GetName().Name, false));
           }
        config.UsingRabbitMq((context, configRabbit) =>
        {
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
