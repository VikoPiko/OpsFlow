using Microsoft.Extensions.DependencyInjection;
using OpsFlow.Application.Interfaces;
using OpsFlow.Infrastructure.Engine;
using OpsFlow.Infrastructure.Executors;
using OpsFlow.Infrastructure.NodeExecutors;

namespace OpsFlow.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<INodeExecutor, StartNodeExecutor>();
            services.AddScoped<INodeExecutor, DelayNodeExecutor>();
            services.AddScoped<INodeExecutor, TaskNodeExecutor>();
            services.AddScoped<INodeExecutor, HttpRequestNodeExecutor>();
            services.AddScoped<INodeExecutor, CommandNodeExecutor>();
            services.AddScoped<INodeExecutor, LogNodeExecutor>();
            services.AddScoped<INodeExecutor, EndNodeExecutor>();

            services.AddScoped<WorkflowEngine>();
            services.AddScoped<NodeEngine>();
            services.AddScoped<NodeExecutorRegistry>();

            return services;
        }
    }
}
