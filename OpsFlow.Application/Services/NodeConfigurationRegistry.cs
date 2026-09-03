using Microsoft.Extensions.Caching.Memory;
using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;

namespace OpsFlow.Application.Services
{
    public sealed class NodeConfigurationRegistry
    {
        private readonly Dictionary<NodeType, INodeConfigurationHandler> _handlers;
        private readonly IMemoryCache cache;

        public NodeConfigurationRegistry(IEnumerable<INodeConfigurationHandler> handlers, IMemoryCache cache)
        {
            _handlers = handlers.ToDictionary(h => h.Type, h => h);
            this.cache = cache;
        }

        public NodeConfiguration GetConfiguration(NodeType type, Guid id)
        {
            var cachedNode = cache.TryGetValue($"nodes:{id}", out var cached);
            if (cached is NodeConfiguration configuration)
            {
                return configuration;
            }

            throw new InvalidOperationException($"Configuration not found for node type {type} and id {id}");
        }

        public INodeConfigurationHandler GetHandler(NodeType type)
        {
            if (!_handlers.TryGetValue(type, out var handler))
            {
                throw new InvalidOperationException($"No handler registered for node type {type}");
            }

            return handler;
        }
    }
}
