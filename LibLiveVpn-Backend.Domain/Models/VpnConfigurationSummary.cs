namespace LibLiveVpn_Backend.Domain.Models
{
    public class VpnConfigurationSummary
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Provider { get; set; } = string.Empty;
    }
}
