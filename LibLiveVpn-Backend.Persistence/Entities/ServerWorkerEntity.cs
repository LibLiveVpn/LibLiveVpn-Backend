namespace LibLiveVpn_Backend.Persistence.Entities
{
    public class ServerWorkerEntity
    {
        public Guid Id { get; set; }

        public string Ip { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
