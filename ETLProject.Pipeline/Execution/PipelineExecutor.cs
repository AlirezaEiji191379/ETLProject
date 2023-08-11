using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using ETLProject.Common.Table;
using ETLProject.Pipeline.Abstractions;
using ETLProject.Pipeline.Execution.PluginExecutions.Factory;
using ETLProject.Pipeline.Graph;

namespace ETLProject.Pipeline.Execution;

public class PipelineExecutor
{
    private DataPipelineGraph _graph;
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<ETLTable>> _nodeExecutionsById;
    private CancellationTokenSource _cancellationTokenSource;
    private IPluginRunnerFactory _pluginRunnerFactory;

    public PipelineExecutor(DataPipelineGraph graph)
    {
        _pluginRunnerFactory = new PluginRunnerFactory();
        _graph = graph;
        _nodeExecutionsById = new ConcurrentDictionary<Guid, TaskCompletionSource<ETLTable>>();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task RunGraph(Guid nodeId)
    {
        _nodeExecutionsById.TryAdd(nodeId,new TaskCompletionSource<ETLTable>());
        await RunGraphInternal(nodeId);
    }
    
    private async Task RunGraphInternal(Guid nodeId)
    {
        var previousNodes = _graph.GetNodeDependencies(nodeId);        
        await RunPreviousNodes(nodeId,previousNodes,_cancellationTokenSource.Token);
        var currentPlugin = _graph.GetNode(nodeId);
        var pluginRunner = _pluginRunnerFactory.GetPluginRunner(currentPlugin.PluginType,currentPlugin.PluginConfig);
        foreach (var lastNode in previousNodes)
        {
            _nodeExecutionsById.TryGetValue(lastNode.PluginId, out var tcs);
            pluginRunner.AddInputEtlTable(tcs.Task.Result);
        }
        var result = await pluginRunner.RunPlugin();
        _nodeExecutionsById.TryGetValue(nodeId, out var currentTcs);
        currentTcs.SetResult(result);
    }

    private async Task RunPreviousNodes(Guid nodeId,List<IPlugin> previousNodes,CancellationToken cancellationToken)
    {
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
            if (_nodeExecutionsById.TryAdd(lastNode.PluginId, new TaskCompletionSource<ETLTable>()))
            {
                await RunGraphInternal(lastNode.PluginId);
            }
            else
            {
                _nodeExecutionsById.TryGetValue(lastNode.PluginId, out var submittedExecution);
                await submittedExecution.Task.WaitAsync(cancellationToken);
            }
        });

    }
}