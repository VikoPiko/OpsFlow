using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.NodeExecutors
{
    public class CommandNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.Command;

        public Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            return Task.FromResult("Command Node Executed");
        }
    }
}
