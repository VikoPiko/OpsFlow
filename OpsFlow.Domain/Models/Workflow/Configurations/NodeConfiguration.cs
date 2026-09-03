namespace OpsFlow.Domain.Models.Workflow.Configurations
{
    public abstract record NodeConfiguration
    {
        public abstract NodeType NodeType { get; }
        public abstract void Validate();
    }
}
