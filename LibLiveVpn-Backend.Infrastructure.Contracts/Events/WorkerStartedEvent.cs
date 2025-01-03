namespace LibLiveVpn_Backend.Infrastructure.Contracts.Events
{
    public record WorkerStartedEvent
    {
        public string WorkerId { get; init; } = null!;
    }
}
