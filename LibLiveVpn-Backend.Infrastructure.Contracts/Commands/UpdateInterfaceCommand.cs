namespace LibLiveVpn_Backend.Infrastructure.Contracts.Commands
{
    public record UpdateInterfaceCommand
    {
        public string InterfaceName { get; init; } = null!;

        public string Configuration { get; init; } = null!;
    }
}
