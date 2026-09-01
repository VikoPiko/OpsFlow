using Microsoft.Extensions.Logging;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.Engine
{
    public class WorkflowEngine(NodeExecutorRegistry executorRegistry, ILogger<WorkflowEngine> logger)
    {
        public async Task ExecuteAsync(Workflow workflow, CancellationToken cancellationToken)
        {
            var currentNode = workflow.Nodes.Single(n => n.Type == NodeType.Start);

            try
            {
                while (currentNode is not null)
                {
                    var random = new Random();
                    var value = random.Next(1, 11);
                    if (value == 5)
                    {
                        throw new Exception("Failed");
                    }
                    var executor = executorRegistry.GetExecutor(currentNode.Type);

                    await executor.ExecuteAsync(currentNode, cancellationToken);

                    if (currentNode.Type == NodeType.End)
                        break;

                    currentNode = GetNextNode(workflow, currentNode) ?? throw new InvalidOperationException($"Node {currentNode.Id} has no valid next node");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static WorkflowNode? GetNextNode(
        Workflow workflow,
        WorkflowNode currentNode)
        {
            var edge = workflow.Edges
                .SingleOrDefault(x => x.FromNode == currentNode.Id);

            if (edge is null)
                return null;

            return workflow.Nodes
                .SingleOrDefault(x => x.Id == edge.ToNode);
        }
    }
}
