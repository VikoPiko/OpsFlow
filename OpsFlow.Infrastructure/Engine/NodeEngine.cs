using Microsoft.Extensions.Logging;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.Engine
{
    public sealed class NodeEngine(
        NodeExecutorRegistry nodeRegistry,
        ILogger<NodeEngine> logger)
    {
        public async Task ExecuteNodeAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            var executor = nodeRegistry.GetExecutor(node.Type)
                ?? throw new InvalidOperationException("No executor found for type: " + node.Type);

            try
            {
                logger.LogInformation("Executing node: {NodeId}", node.Id);
                await executor.ExecuteAsync(node, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                logger.LogError("Error occurred while executing node: {NodeId}", node.Id);
                throw;
            }

            return;
        }
    }
}
