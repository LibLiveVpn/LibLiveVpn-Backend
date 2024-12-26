namespace LibLiveVpn_Backend.Persistence.Entities
{
    public class ServerEntity
    {
        public Guid Id { get; set; }

        public string Ip { get; set; } = null!;

        public int Port { get; set; } = -1;

        public string Provider { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
