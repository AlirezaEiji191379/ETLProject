using ETLProject.Pipeline.Common;

namespace ETLProject.Pipeline.Abstractions;

public interface IPipelineContainer
{
     DataPipeline GetDataPipeLine(Guid pipelineId);
}