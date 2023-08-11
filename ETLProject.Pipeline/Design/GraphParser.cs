using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Contract.DBReader;
using ETLProject.Contract.Pipeline;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Graph;
using ETLProject.Pipeline.Plugins;

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
            AddToNodeList(nodeNames, edge);
            //AddToSourceCountDictionary(inputEdgesCount, edge);
            //AddToDestinationCountDictionary(outputEdgesCount, edge);
        }
        //CheckInputs(inputEdgesCount);
        //CheckOutputs(outputEdgesCount);
        CreateGraph(graphDto, nodeNames, graph);
        _pipelineContainer.AddPipeline(graph);
        return graph;
    }

    private void CreateGraph(GraphDto graphDto, List<string> nodeNames, DataPipelineGraph graph)
    {
        foreach (var node in nodeNames)
        {
            var plugin = CreatePlugin(node, graphDto.PluginConfigs);
            graph.AddVertex(plugin);
        }

        foreach (var edge in graphDto.Edges)
        {
            var inputNode = graph.GetNodeByTitle(edge.Src);
            var outputNode = graph.GetNodeByTitle(edge.Dst);
            graph.AddEdge(inputNode, outputNode);
        }
    }

    private static void AddToNodeList(List<string> nodeNames, EdgeDto edge)
    {
        if (!nodeNames.Contains(edge.Src))
        {
            nodeNames.Add(edge.Src);
        }

        if (!nodeNames.Contains(edge.Dst))
        {
            nodeNames.Add(edge.Dst);
        }
    }

    private void CheckInputs(Dictionary<string, int> inputEdgesCount)
    {
        foreach (var source in inputEdgesCount.Keys)
        {
            if (source.Contains("Read"))
            {
                if (inputEdgesCount[source] != 0)
                {
                    throw new Exception("db reader can not have input!");
                }
            }
            else if (source.Contains("Write"))
            {
                if (inputEdgesCount[source] != 1)
                {
                    throw new Exception("db writer can not have more than one input!");
                }
            }
            else if (source.Contains("Order"))
            {
                if (inputEdgesCount[source] != 1)
                {
                    throw new Exception("order can not have more than one input!");
                }
            }
            else if (source.Contains("Limit"))
            {
                if (inputEdgesCount[source] != 1)
                {
                    throw new Exception("limit can not have more than one input!");
                }
            }
            else if (source.Contains("Where"))
            {
                if (inputEdgesCount[source] != 1)
                {
                    throw new Exception("where can not have more than one input!");
                }
            }
            else if (source.Contains("Agg"))
            {
                if (inputEdgesCount[source] != 1)
                {
                    throw new Exception("Agg can not have more than one input!");
                }
            }
            else if (source.Contains("Distinct"))
            {
                if (inputEdgesCount[source] != 1)
                {
                    throw new Exception("distinct can not have more than one input!");
                }
            }
        }
    }

    private void CheckOutputs(Dictionary<string, int> outputEdgesCount)
    {
        foreach (var destination in outputEdgesCount.Keys)
        {
            if (destination.Contains("Read"))
            {
            }
            else if (destination.Contains("Write"))
            {
                if (outputEdgesCount[destination] != 0)
                {
                    throw new Exception("db writer can not have more than one output!");
                }
            }
            else if (destination.Contains("Order"))
            {
            }
            else if (destination.Contains("Limit"))
            {
            }
            else if (destination.Contains("Where"))
            {
            }
            else if (destination.Contains("Agg"))
            {
            }
            else if (destination.Contains("Distinct"))
            {
            }
        }
    }

    private void AddToSourceCountDictionary(IDictionary<string, int> edgesCount, EdgeDto edge)
    {
        if (edgesCount.ContainsKey(edge.Dst))
        {
            edgesCount[edge.Dst]++;
        }
        else
        {
            edgesCount.Add(edge.Dst, 1);
        }
    }

    private void AddToDestinationCountDictionary(IDictionary<string, int> edgesCount, EdgeDto edge)
    {
        if (edgesCount.ContainsKey(edge.Src))
        {
            edgesCount[edge.Src]++;
        }
        else
        {
            edgesCount.Add(edge.Src, 1);
        }
    }

    private IPlugin CreatePlugin(string pluginTitle, Dictionary<string, PluginConfigDto> pluginConfigDtos)
    {
        var config = pluginConfigDtos[pluginTitle];
        if (pluginTitle.Contains("Read"))
        {
            return new DbReaderPlugin(pluginTitle, config.DbReader);
        }
        else if (pluginTitle.Contains("Write"))
        {
            return new DbAddPlugin(pluginTitle, config.DbWriter);
        }
        else if (pluginTitle.Contains("Limit"))
        {
            return new LimitPlugin(pluginTitle, config.LimitContract);
        }
        else if (pluginTitle.Contains("Order"))
        {
            return new OrderPlugin(pluginTitle, config.SortContract);
        }
        else if (pluginTitle.Contains("Where"))
        {
            return new WherePlugin(pluginTitle, config.Condition);
        }
        else if (pluginTitle.Contains("Agg"))
        {
            return new AggPlugin(pluginTitle, config.Agg);
        }
        else if (pluginTitle.Contains("Distinct"))
        {
            return new DistinctPlugin(pluginTitle);
        }
        return null;
    }
}