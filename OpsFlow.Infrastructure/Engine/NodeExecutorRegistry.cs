using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;

namespace OpsFlow.Infrastructure.Engine
{
    public class NodeExecutorRegistry
    {
        private readonly Dictionary<NodeType, INodeExecutor> _executors;

        public NodeExecutorRegistry(IEnumerable<INodeExecutor> executors)
        {
            _executors = executors.ToDictionary(e => e.Type, e => e);
        }

        public INodeExecutor GetExecutor(NodeType nodeType)
        {
            if (!_executors.TryGetValue(nodeType, out var executor))
            {
                throw new InvalidOperationException($"No executor found for node type: {nodeType}");
            }
            return executor;
        }
    }
}
