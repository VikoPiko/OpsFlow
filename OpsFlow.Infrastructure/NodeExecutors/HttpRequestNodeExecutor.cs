using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.NodeExecutors
{
    public class HttpRequestNodeExecutor : INodeExecutor
    {
        public NodeType Type => NodeType.HttpRequest;

        public Task ExecuteAsync(WorkflowNode node, CancellationToken cancellationToken)
        {
            return Task.FromResult("HttpRequest Node Executed");
        }
    }
}
