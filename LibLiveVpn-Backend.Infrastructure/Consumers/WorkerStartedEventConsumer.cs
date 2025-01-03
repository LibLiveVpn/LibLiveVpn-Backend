using LibLiveVpn_Backend.Infrastructure.Contracts.Commands;
using LibLiveVpn_Backend.Infrastructure.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LibLiveVpn_Backend.Infrastructure.Consumers
{
    public class WorkerStartedEventConsumer : IConsumer<WorkerStartedEvent>
    {
        private readonly ILogger<CommandExecudedEventConsumer> _logger;
        private readonly IBus _bus;

        public WorkerStartedEventConsumer(ILogger<CommandExecudedEventConsumer> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<WorkerStartedEvent> context)
        {
            var workerEndpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/message-queue1"));

            await workerEndpoint.Send(new CreateInterfaceCommand
            {
                InterfaceName = "wg1",
                Configuration = "[Interface]\r\nPrivateKey = IL0BG0kJknqEc5H5MrhPtGPeA6KJ6ZRc3pIv+in5an8=\r\nAddress = 10.0.0.1/24\r\nListenPort = 51820\r\nPostUp = iptables -A FORWARD -i %i -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE\r\nPostDown = iptables -D FORWARD -i %i -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE\r\n\r\n# Peer 1\r\n[Peer]\r\nPublicKey = L9/1utpRZ3WxaEp05JISUVCnB2ynidcovN7mQcvJPm8=\r\nAllowedIPs = 10.0.0.2/32\r\n\r\n# Peer 2\r\n[Peer]\r\nPublicKey = YIt0UV6+FrPK8WTp5Wpxeqrb8Y88ipPbNkVvJEuftwM=\r\nAllowedIPs = 10.0.0.3/32\r\n\r\n# Peer 3\r\n[Peer]\r\nPublicKey = dBDTBovs3oejixnw5obI9/T0NqDkK4PfUnk8ikmqCgo=\r\nAllowedIPs = 10.0.0.4/32"
            });
        }
    }
}
