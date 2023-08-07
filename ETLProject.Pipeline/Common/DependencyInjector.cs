using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Graph;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Pipeline.Common;

public static class DependencyInjector
{
    public static IServiceCollection AddPipelineServices(this IServiceCollection services)
    {
        services.AddSingleton<IPipelineContainer,PipelineContainer>();
        return services;
    }
}