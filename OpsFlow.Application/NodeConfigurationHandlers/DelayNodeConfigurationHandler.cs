using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;
using System.Text.Json;

namespace OpsFlow.Application.NodeConfigurationHandlers
{
    public class DelayNodeConfigurationHandler : INodeConfigurationHandler
    {
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NodeType Type => NodeType.Delay;

        public NodeConfiguration Create(JsonElement configuration)
        {
            var result = configuration.Deserialize<DelayConfiguration>(options);

            return result ?? throw new InvalidOperationException("Unable to deserialize configuration for Delay node.");
        }
    }
}
