namespace LibLiveVpn_Backend.Persistence.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
