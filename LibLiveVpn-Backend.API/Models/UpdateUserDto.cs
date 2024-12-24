namespace LibLiveVpn_Backend.API.Models
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
