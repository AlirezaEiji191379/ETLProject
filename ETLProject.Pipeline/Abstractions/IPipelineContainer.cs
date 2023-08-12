using ETLProject.Pipeline.Graph;

namespace ETLProject.Pipeline.Abstractions;

public interface IPipelineContainer
{
    DataPipelineGraph GetPipeline(Guid pipelineId);
    void AddPipeline(DataPipelineGraph pipeline);
}