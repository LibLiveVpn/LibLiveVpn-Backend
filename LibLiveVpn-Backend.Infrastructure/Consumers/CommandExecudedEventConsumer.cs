using System.Text.Json;
using LibLiveVpn_Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LibLiveVpn_Backend.Infrastructure.Consumers
{
    public class CommandExecudedEventConsumer : IConsumer<CommandExecudedEvent>
    {
        private readonly ILogger<CommandExecudedEventConsumer> _logger;

        public CommandExecudedEventConsumer(ILogger<CommandExecudedEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CommandExecudedEvent> context)
        {
            var jsonString = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { WriteIndented = true });
            _logger.LogInformation($"Command event:\n{jsonString}");

            return Task.CompletedTask;
        }
    }
}
