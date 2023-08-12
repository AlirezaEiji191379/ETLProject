using ETLProject.Pipeline.Abstractions;

namespace ETLProject.Pipeline.Graph;

public class PipelineContainer : IPipelineContainer
{
    private Dictionary<Guid, DataPipelineGraph> _pipelineGraphById;

    public PipelineContainer()
    {
        _pipelineGraphById = new Dictionary<Guid, DataPipelineGraph>();
    }

    public DataPipelineGraph GetPipeline(Guid pipelineId)
    {
        return _pipelineGraphById[pipelineId];
    }

    public void AddPipeline(DataPipelineGraph pipeline)
    {
        _pipelineGraphById.Add(pipeline.PipelineId, pipeline);
    }
}