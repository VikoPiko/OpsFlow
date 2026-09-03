using Microsoft.Extensions.DependencyInjection;
using OpsFlow.Application.Interfaces;
using OpsFlow.Application.NodeConfigurationHandlers;
using OpsFlow.Application.Services;

namespace OpsFlow.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INodeConfigurationFactory, NodeConfigurationFactory>();

            services.AddScoped<INodeConfigurationHandler, DelayNodeConfigurationHandler>();
            services.AddScoped<INodeConfigurationHandler, LogNodeConfigurationHandler>();

            services.AddScoped<NodeConfigurationRegistry>();

            services.AddScoped<WorkflowNodeService>();

            return services;
        }
    }
}
