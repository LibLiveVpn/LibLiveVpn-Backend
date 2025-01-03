namespace LibLiveVpn_Backend.Infrastructure.Models
{
    public class MessageBrokerConfiguration
    {
        public const string Section = "MessageBrokerSettings";

        public string Host { get; set; } = null!;

        public ushort Port { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
