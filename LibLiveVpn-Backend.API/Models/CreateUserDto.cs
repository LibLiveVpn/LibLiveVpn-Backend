namespace LibLiveVpn_Backend.API.Models
{
    public class CreateUserDto
    {
        public string Login { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
