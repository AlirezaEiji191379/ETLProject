using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;
using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Common;

public class DataPipeline
{
    public Guid PipelineId { get; init; }
    public Dictionary<Guid, IPlugin> PluginsById { get; }
    public DataPipeline()
    {
        PipelineId = new Guid();
        PluginsById = new Dictionary<Guid, IPlugin>();
    }
}
