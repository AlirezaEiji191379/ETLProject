using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Contract.Pipeline;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Graph;

namespace ETLProject.Pipeline.Design;

public class GraphParser : IGraphParser
{
    private readonly IPipelineContainer _pipelineContainer;

    public GraphParser(IPipelineContainer pipelineContainer)
    {
        _pipelineContainer = pipelineContainer;
    }

    public DataPipelineGraph ParseGraphString(GraphDto graphDto)
    {
        var graph = new DataPipelineGraph();
        var nodeNames = new List<string>();
        var inputEdgesCount = new Dictionary<string, int>();
        var outputEdgesCount = new Dictionary<string, int>();
        foreach (var edge in graphDto.Edges)
        {
            // check input outputs
            
            // create plugins
            
            // Add plugins to graph
        }

        _pipelineContainer.AddPipeline(graph);
        return graph;
    }

    private IPlugin CreatePlugin(string pluginTitle,Dictionary<string,PluginConfigDto> pluginConfigDtos)
    {
        return null;
    }
    
}