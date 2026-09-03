using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;
using System.Text.Json;

namespace OpsFlow.Application.Interfaces
{
    public interface INodeConfigurationFactory
    {
        NodeConfiguration? Create(NodeType Type, JsonElement? configuration);
    }
}
