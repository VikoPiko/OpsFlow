using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Application.Interfaces
{
    public interface INodeExecutor
    {
        NodeType Type { get; }

        Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken);
    }
}
