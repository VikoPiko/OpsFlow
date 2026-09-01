using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.Executors
{
    public class DelayNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.Delay;

        public async Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            var delayValue = node.Configuration?.GetProperty("seconds").GetInt32() ?? 1;

            Console.WriteLine($"Executing Delay Node: {node.Id} with delay of {delayValue} seconds");

            Console.WriteLine(
                $"[{DateTimeOffset.UtcNow:HH:mm:ss.fff}] " +
                $"Starting Delay Node {node.Id} for {delayValue} seconds");

            await Task.Delay(
                TimeSpan.FromSeconds(delayValue),
                cancellationToken);

            Console.WriteLine(
                $"[{DateTimeOffset.UtcNow:HH:mm:ss.fff}] " +
                $"Finished Delay Node {node.Id}");
        }
    }
}
