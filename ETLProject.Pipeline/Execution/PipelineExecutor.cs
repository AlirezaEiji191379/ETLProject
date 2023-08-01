using System.Collections.Concurrent;
using ETLProject.Pipeline.Graph;

namespace ETLProject.Pipeline.Execution;

public class PipelineExecutor
{
    private DataPipelineGraph _graph;
    private readonly ConcurrentDictionary<Guid, Task> _nodeExecutionsById;
    private CancellationTokenSource _cancellationTokenSource;

    public PipelineExecutor(DataPipelineGraph graph)
    {
        _graph = graph;
        _nodeExecutionsById = new ConcurrentDictionary<Guid, Task>();
        _cancellationTokenSource = new CancellationTokenSource();
    }
    
    public async Task RunGraph(Guid nodeId)
    {
        
        await RunPreviousNodes(nodeId, _cancellationTokenSource.Token);
        var isFound = _nodeExecutionsById.TryGetValue(nodeId, out var nodeTask);
        if (!isFound)
        {
            nodeTask = new PluginExecutionTask(nodeId).RunPlugin();
        }
        if (nodeTask.Status == TaskStatus.Created)
        {
            nodeTask.Start();
        }
        await nodeTask;
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
            if (_nodeExecutionsById.TryAdd(lastNode.PluginId, new PluginExecutionTask(lastNode.PluginId).RunPlugin()))
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