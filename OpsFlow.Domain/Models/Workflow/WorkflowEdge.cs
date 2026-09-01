namespace OpsFlow.Domain.Models.Workflow
{
    public sealed record WorkflowEdge
    {
        public Guid FromNode { get; set; }
        public Guid ToNode { get; set; }
    }
}
