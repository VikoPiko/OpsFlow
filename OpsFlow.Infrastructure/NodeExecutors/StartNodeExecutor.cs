using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.NodeExecutors
{
    public class StartNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.Start;

        public Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Executing Start Node: {node.Id}");
            return Task.FromResult("Start Node Executed");
        }
    }
}
