using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.NodeExecutors
{
    public class TaskNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.Task;

        public Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            return Task.FromResult("Completed");
        }
    }
}
