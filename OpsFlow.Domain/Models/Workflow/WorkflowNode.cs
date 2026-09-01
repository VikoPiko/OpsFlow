using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpsFlow.Domain.Models.Workflow
{
    public sealed record WorkflowNode
    {
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public NodeType Type { get; init; }

        //public Dictionary<string, object>? Configuration { get; init; }
        public JsonElement? Configuration { get; init; }
    }
}
