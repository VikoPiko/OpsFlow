using OpsFlow.Domain.Models.Workflow;
using System.Text.Json;

namespace OpsFlow.Domain.Models.Requests
{
    public sealed record WorkflowNodeCreateRequest
    {
        public NodeType Type { get; init; }

        public JsonElement? Configuration { get; init; }
    }
}