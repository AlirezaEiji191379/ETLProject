using System.Collections.Concurrent;
using ETLProject.Common.Table;
using ETLProject.Pipeline.Graph;

namespace ETLProject.Pipeline.Execution;

public class PipelineExecutor
{
    private DataPipelineGraph _graph;
    private readonly ConcurrentDictionary<Guid, Task<ETLTable>> _nodeExecutionsById;
    private CancellationTokenSource _cancellationTokenSource;

    public PipelineExecutor(DataPipelineGraph graph)
    {
        _graph = graph;
        _nodeExecutionsById = new ConcurrentDictionary<Guid, Task<ETLTable>>();
        _cancellationTokenSource = new CancellationTokenSource();
    }
    
    public async Task RunGraph(Guid nodeId)
    {
        
        await RunPreviousNodes(nodeId, _cancellationTokenSource.Token);
        var isFound = _nodeExecutionsById.TryGetValue(nodeId, out Task<ETLTable> nodeTask);
        if (!isFound)
        {
            nodeTask = new PluginExecutionTask(_graph.GetNode(nodeId)).RunPlugin();
        }
        if (nodeTask.Status == TaskStatus.Created)
        {
            nodeTask.Start();
        }

        var etlTable = nodeTask.Result;
        _graph.GetNextPlugins(nodeId).ForEach(x => x.AddInputTable(etlTable));
    }

    private async Task RunPreviousNodes(Guid nodeId, CancellationToken cancellationToken)
    {
        var previousNodes = _graph.GetNodeDependencies(nodeId);
        if (previousNodes.Count == 0)
        {
            return;
        }

        var parallel = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 3
        };
        await Parallel.ForEachAsync(previousNodes,parallel , async (lastNode,cancellationToken) =>
        {
            if (_nodeExecutionsById.TryAdd(lastNode.PluginId, new PluginExecutionTask(lastNode).RunPlugin()))
            {
                await RunGraph(lastNode.PluginId);
            }
            else
            {
                _nodeExecutionsById.TryGetValue(lastNode.PluginId, out var submittedTask);
                await submittedTask;
            }
        });

    }
}