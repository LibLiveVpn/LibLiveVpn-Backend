namespace LibLiveVpn_Backend.Domain
{
    public class VpnConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Provider { get; set; } = string.Empty;
    }
}
