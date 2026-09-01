using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.NodeExecutors
{
    public class EndNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.End;

        public Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Executing End Node: {node.Id}");
            return Task.FromResult("End Node Executed");
        }
    }
}
