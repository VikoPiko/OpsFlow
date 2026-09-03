namespace OpsFlow.Domain.Models.Workflow.Configurations
{
    public sealed record StartConfiguration : NodeConfiguration
    {
        public override NodeType NodeType => NodeType.Start;

        public override void Validate()
        {
            return;
        }
    }
}
