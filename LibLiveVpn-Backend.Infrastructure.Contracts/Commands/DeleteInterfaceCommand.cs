namespace LibLiveVpn_Backend.Infrastructure.Contracts.Commands
{
    public record DeleteInterfaceCommand
    {
        public string InterfaceName { get; init; } = null!;
    }
}
