using OpsFlow.Domain.Exceptions;
using OpsFlow.Domain.Models.Workflow.Configurations;
using System.Text.Json.Serialization;

namespace OpsFlow.Domain.Models.Workflow
{
    public sealed record WorkflowNode
    {
        public Guid Id { get; set; }
        public string? Name { get; init; }

        [JsonPropertyName("type")]
        public NodeType Type { get; init; }

        //public Dictionary<string, object>? Configuration { get; init; }
        public NodeConfiguration? Configuration { get; init; }

        public static WorkflowNode? Create(
            string? name,
            NodeType type,
            NodeConfiguration? configuration)
        {

            ValidateConfiguration(type, configuration);

            var Id = Guid.NewGuid();

            return new WorkflowNode
            {
                Id = Id,
                Name = name ?? $"Node-{Id}",
                Type = type,
                Configuration = configuration
            };
        }

        public static void ValidateConfiguration(NodeType type, NodeConfiguration? configuration)
        {
            if (configuration is null)
                return;

            if (configuration.NodeType != type)
            {
                throw new DomainException(
                    $"Configuration type '{configuration.NodeType}' " +
                    $"does not match node type '{type}'.");
            }

            configuration.Validate();
        }
    }
}
