using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Graph;

public class PluginExecutionTask
{
    public Guid PluginId { get; init; }

    public PluginExecutionTask(Guid pluginId)
    {
        PluginId = pluginId;
    }


    public Task RunPlugin()
    {   
        return new Task(async () =>
        {
            Console.WriteLine($"Node {PluginId} is Done!");
            await Task.CompletedTask;
        });
    }
    
}