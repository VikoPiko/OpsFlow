using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Exceptions;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;
using System.Text.Json;

namespace OpsFlow.Application.Services
{
    public class NodeConfigurationFactory(
        NodeConfigurationRegistry registry) : INodeConfigurationFactory
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NodeConfiguration? Create(NodeType type, JsonElement? configuration)
        {
            if (type is NodeType.Start or NodeType.End)
            {
                if (configuration is not null)
                {
                    throw new DomainException(
                        $"{type} node does not support configuration.");
                }

                return null;
            }

            if (configuration is null)
            {
                throw new DomainException(
                    $"{type} node requires configuration.");
            }

            var handler = registry.GetHandler(type);

            return handler.Create(configuration.Value);
        }
    }
}
