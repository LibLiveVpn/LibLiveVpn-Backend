namespace LibLiveVpn_Backend.Persistence
{
    public class DbConfiguration
    {
        public const string Section = "DatabaseConnectionSettings";

        public string Host { get; set; } = null!;

        public int Port { get; set; }

        public string Database { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
