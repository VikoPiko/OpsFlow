using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;

namespace OpsFlow.Infrastructure.Executors
{
    public class DelayNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.Delay;

        public async Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            if (node.Configuration is not DelayConfiguration configuration)
                throw new InvalidOperationException("Invalid configuration for Delay node.");

            Console.WriteLine(
                $"[{DateTimeOffset.UtcNow:HH:mm:ss.fff}] " +
                $"Starting Delay Node {node.Id} for {configuration.Seconds} seconds");

            await Task.Delay(
                TimeSpan.FromSeconds(configuration.Seconds),
                cancellationToken);

            Console.WriteLine(
                $"[{DateTimeOffset.UtcNow:HH:mm:ss.fff}] " +
                $"Finished Delay Node {node.Id}");
        }
    }
}
