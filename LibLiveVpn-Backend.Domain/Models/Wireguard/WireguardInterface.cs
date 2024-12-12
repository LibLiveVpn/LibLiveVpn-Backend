namespace LibLiveVpn_Backend.Domain.Models.Wireguard
{
    public class WireguardInterface
    {
        public string PrivateKey { get; set; } = string.Empty;

        public string AddressIPv4 { get; set; } = string.Empty;

        public string AddressIPv6 { get; set; } = string.Empty;

        public int ListenPort { get; set; } = -1;

        public string PostUp { get; set; } = string.Empty;

        public string PostDown { get; set; } = string.Empty;

        public List<string> Dns { get; set; } = new List<string>();

        public int Mtu { get; set; } = -1;
    }
}
