namespace LibLiveVpn_Backend.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<VpnConfigurationSummary> Configurations { get; set; } = new List<VpnConfigurationSummary>();
    }
}
