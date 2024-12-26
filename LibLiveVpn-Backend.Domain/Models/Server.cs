namespace LibLiveVpn_Backend.Domain.Models
{
    public class Server
    {
        public Guid Id { get; set; }

        public string Ip { get; set; } = null!;

        public int Port { get; set; } = -1;

        public string Provider { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
