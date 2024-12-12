namespace LibLiveVpn_Backend.Domain.Models
{
    public class VpnNetworkArea
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string BaseIpAddress { get; set; } = string.Empty;

        public int Members { get; set; }

        public int MembersLimit { get; set; }
    }
}
