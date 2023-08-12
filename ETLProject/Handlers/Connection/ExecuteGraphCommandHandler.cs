using ETLProject.Contract;
using ETLProject.Contract.Pipeline;
using ETLProject.Pipeline;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Execution;
using MediatR;

namespace ETLProject.Handlers.Connection;

public class ExecuteGraphCommandHandler : IRequestHandler<GraphDto,ResponseDto>
{
    private readonly IGraphParser _graphParser;
    private readonly IServiceProvider _serviceProvider;

    public ExecuteGraphCommandHandler(IGraphParser graphParser, IServiceProvider serviceProvider)
    {
        _graphParser = graphParser;
        _serviceProvider = serviceProvider;
    }


    public async Task<ResponseDto> Handle(GraphDto request, CancellationToken cancellationToken)
    {
        ServiceProviderContainer.ServiceProvider = _serviceProvider;
        try
        {
            var pipeline = _graphParser.ParseGraph(request);
            var executor = new PipelineExecutor(pipeline);
            var runningNode = pipeline.GetNodeByTitle(request.RunningPlugin);
            await executor.RunGraph(runningNode.PluginId);
            return new ResponseDto()
            {
                StatusCode = 200,
                Message = new {Message = "the etl pipeline executed successfully!"}
            };
        }
        catch (Exception e)
        {
            return new ResponseDto()
            {
                StatusCode = 400,
                Message = e.Message//new {Message ="error in graph parsing and execution of graph"}
            };
        }
    }
}