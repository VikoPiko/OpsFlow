namespace OpsFlow.Domain.Models.Workflow.Configurations
{
    public sealed record EndConfiguration : NodeConfiguration
    {
        public override NodeType NodeType => NodeType.End;

        public override void Validate()
        {
            return;
        }
    }
}
