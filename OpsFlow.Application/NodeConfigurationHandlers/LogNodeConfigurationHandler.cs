using OpsFlow.Application.Interfaces;
using OpsFlow.Domain.Models.Workflow;
using OpsFlow.Domain.Models.Workflow.Configurations;
using System.Text.Json;

namespace OpsFlow.Application.NodeConfigurationHandlers
{
    public class LogNodeConfigurationHandler : INodeConfigurationHandler
    {
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NodeType Type => NodeType.Log;

        public NodeConfiguration Create(JsonElement configuration)
        {
            var result = configuration.Deserialize<LogConfiguration>(options);

            return result ?? throw new InvalidOperationException("Unable to deserialize configuration for Log node.");
        }
    }
}
