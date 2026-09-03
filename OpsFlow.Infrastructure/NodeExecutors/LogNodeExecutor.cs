using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;

namespace OpsFlow.Infrastructure.NodeExecutors
{
    public class LogNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.Log;

        public Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            if (node.Configuration is not LogConfiguration config)
                throw new InvalidOperationException(" nvalid configuration for Log node.");

            Console.WriteLine($"Executing Log Node: {node.Id} with message: {config.Message}");
            return Task.FromResult($"Log Node Executed with message: {config.Message}");
        }
    }
}
