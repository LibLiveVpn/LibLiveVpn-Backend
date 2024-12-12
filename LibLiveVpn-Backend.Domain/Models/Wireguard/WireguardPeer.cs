namespace LibLiveVpn_Backend.Domain.Models.Wireguard
{
    public class WireguardPeer
    {
        public string PrivateKey { get; set; } = string.Empty;

        public string AddressIPv4 { get; set; } = string.Empty;

        public string AddressIPv6 { get; set; } = string.Empty;

        public List<string> AllowedIPs { get; set; } = new List<string>();

        public string PresharedKey { get; set; } = string.Empty;

        public int PersistentKeepalive { get; set; } = -1;

        public string Endpoint { get; set; } = string.Empty;
    }
}
