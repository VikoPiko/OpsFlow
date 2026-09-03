using OpsFlow.Domain.Exceptions;

namespace OpsFlow.Domain.Models.Workflow.Configurations
{
    public sealed record DelayConfiguration : NodeConfiguration
    {
        public override NodeType NodeType => NodeType.Delay;
        public required int Seconds { get; init; }

        public override void Validate()
        {
            if (Seconds <= 0)
                throw new DomainException("Delay seconds must be a positive integer");
            if (Seconds >= 86_400)
            {
                throw new DomainException("Delay seconds must be less than 86,400 (24 hours)");
            }
        }
    }
}
