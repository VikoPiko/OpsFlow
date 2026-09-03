using OpsFlow.Domain.Exceptions;

namespace OpsFlow.Domain.Models.Workflow.Configurations
{
    public sealed record TaskConfiguration : NodeConfiguration
    {
        public override NodeType NodeType => NodeType.Task;
        public required string TaskName { get; init; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(TaskName))
                throw new DomainException("Task name cannot be null or empty");
        }
    }
}
