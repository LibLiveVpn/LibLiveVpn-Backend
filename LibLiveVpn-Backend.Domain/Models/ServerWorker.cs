namespace LibLiveVpn_Backend.Domain.Models
{
    public class ServerWorker
    {
        public Guid Id { get; set; }

        public string Ip { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
