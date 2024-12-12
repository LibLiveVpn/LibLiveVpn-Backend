namespace LibLiveVpn_Backend.Domain.Models.Wireguard
{
    public class WireguardConfiguration
    {
        public Guid Id { get; set; }

        public WireguardConfigurationType ConfigurationType { get; set; } = WireguardConfigurationType.User;

        public WireguardInterface Interface { get; set; } = new WireguardInterface();

        public List<WireguardPeer> Peers { get; set; } = new List<WireguardPeer>();
    }
}
