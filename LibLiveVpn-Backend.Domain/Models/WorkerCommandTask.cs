namespace LibLiveVpn_Backend.Domain.Models
{
    public class WorkerCommandTask
    {
        public Guid Id { get; set; }

        public bool InProccess { get; set; } = true;

        public string CommandName { get; set; } = null!;

        public string InterfaceName { get; set; } = null!;

        public int StatusCode { get; set; } = -1;

        public string Detail { get; set; } = null!;
    }
}
