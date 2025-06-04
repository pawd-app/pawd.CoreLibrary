using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using pawd.CoreLibrary.JobRabbitMqBridge.Models;
using RabbitMQ.Client;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMqMessaging(
        this IServiceCollection services,
        Action<RabbitMqOptions> configureOptions)
    {
        services.Configure(configureOptions);
        
        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
            return new ConnectionFactory
            {
                HostName = options.HostName,
                Port = options.Port,
                UserName = options.Username,
                Password = options.Password,
                VirtualHost = options.VirtualHost,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };
        });

        services.AddSingleton(async sp =>
        {
            var factory = sp.GetRequiredService<ConnectionFactory>();
            return await factory.CreateConnectionAsync();
        });

        services.AddSingleton(sp => sp.GetRequiredService<Task<IConnection>>().Result);

        services.AddScoped<IRabbitMqMessagePublisher, RabbitMqMessagePublisher>();

        return services;
    }
}