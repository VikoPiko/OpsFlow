using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.NodeExecutors
{
    public class LogNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.Log;

        public Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            var config = node.Configuration?.GetProperty("message").GetString() ?? "NotSet";

            Console.WriteLine($"Executing Log Node: {node.Id} with message: {config}");
            return Task.FromResult($"Log Node Executed with message: {config}");
        }
    }
}
