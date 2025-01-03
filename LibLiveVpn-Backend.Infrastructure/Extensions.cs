using LibLiveVpn_Backend.Infrastructure.Consumers;
using LibLiveVpn_Backend.Infrastructure.Models;
using LibLiveVpn_Contracts.Commands;
using LibLiveVpn_Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibLiveVpn_Backend.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransitDependencies(configuration);

            return services;
        }

        private static IServiceCollection AddMassTransitDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var mqBrokerSection = configuration.GetSection(MessageBrokerConfiguration.Section);
            var mqBrokerConfiguration = new MessageBrokerConfiguration();

            mqBrokerConfiguration.Host = mqBrokerSection["Host"] ?? throw new ArgumentNullException($"{MessageBrokerConfiguration.Section}:Host not exists");

            if (ushort.TryParse(mqBrokerSection[nameof(MessageBrokerConfiguration.Port)], out ushort brokerPort))
                mqBrokerConfiguration.Port = brokerPort;
            else
                mqBrokerConfiguration.Port = 5672;

            mqBrokerConfiguration.Username = mqBrokerSection["Username"] ?? throw new ArgumentNullException($"{MessageBrokerConfiguration.Section}:Username not exists");
            mqBrokerConfiguration.Password = mqBrokerSection["Password"] ?? throw new ArgumentNullException($"{MessageBrokerConfiguration.Section}:Password not exists");

            services.AddMassTransit(conf =>
            {
                // Consumers
                conf.AddConsumer<CommandExecudedEventConsumer>();
                conf.AddConsumer<WorkerStartedEventConsumer>();

                conf.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(mqBrokerConfiguration.Host, mqBrokerConfiguration.Port, "/", c =>
                    {
                        c.ConnectionName("backend-connection");
                        c.Username(mqBrokerConfiguration.Username);
                        c.Password(mqBrokerConfiguration.Password);
                    });

                    cfg.Message<CreateInterfaceCommand>(x => x.SetEntityName(nameof(CreateInterfaceCommand)));
                    cfg.Message<UpdateInterfaceCommand>(x => x.SetEntityName(nameof(UpdateInterfaceCommand)));
                    cfg.Message<DeleteInterfaceCommand>(x => x.SetEntityName(nameof(DeleteInterfaceCommand)));

                    cfg.Message<CommandExecudedEvent>(x => x.SetEntityName(nameof(CommandExecudedEvent)));
                    cfg.Message<WorkerStartedEvent>(x => x.SetEntityName(nameof(WorkerStartedEvent)));

                    // Configure consumers endpoints
                    cfg.ReceiveEndpoint("backend", endpoint =>
                    {
                        endpoint.ConfigureConsumer<CommandExecudedEventConsumer>(context);
                        endpoint.ConfigureConsumer<WorkerStartedEventConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
