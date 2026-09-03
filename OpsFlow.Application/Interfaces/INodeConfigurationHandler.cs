using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;
using System.Text.Json;

namespace OpsFlow.Application.Interfaces
{
    public interface INodeConfigurationHandler
    {
        NodeType Type { get; }
        NodeConfiguration Create(JsonElement configuration);
    }
}
