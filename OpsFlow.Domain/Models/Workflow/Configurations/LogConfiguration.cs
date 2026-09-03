using OpsFlow.Domain.Exceptions;

namespace OpsFlow.Domain.Models.Workflow.Configurations
{
    public sealed record LogConfiguration : NodeConfiguration
    {
        public override NodeType NodeType => NodeType.Log;
        public required string Message { get; init; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Message))
                throw new DomainException("Log message cannot be null or empty");
        }
    }
}
