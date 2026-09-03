using Microsoft.Extensions.Caching.Memory;
using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Requests;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Application.Services
{
    public sealed class WorkflowNodeService(
        INodeConfigurationFactory configurationFactory,
        IMemoryCache cache)
    {
        public async Task<WorkflowNode> CreateAsync(WorkflowNodeCreateRequest request, CancellationToken cancellationToken)
        {
            var configuration = configurationFactory.Create(request.Type, request.Configuration);

            var node = WorkflowNode.Create(name: request.Name, type: request.Type, configuration: configuration);

            if (node is null)
                throw new InvalidOperationException("Failed to create workflow node");

            var nodes = cache.Get<List<WorkflowNode>>("nodes") ?? new List<WorkflowNode>();
            nodes.Add(node);
            cache.Set("nodes", nodes);
            return node;
        }
    }
}
