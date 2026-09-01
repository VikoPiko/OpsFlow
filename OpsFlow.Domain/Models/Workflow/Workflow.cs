namespace OpsFlow.Domain.Models.Workflow
{
    public sealed record Workflow
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<WorkflowNode> Nodes { get; set; } = new();
        public List<WorkflowEdge> Edges { get; set; } = new();
    }
}
