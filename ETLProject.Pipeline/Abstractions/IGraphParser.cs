using ETLProject.Contract.Pipeline;
using ETLProject.Pipeline.Graph;

namespace ETLProject.Pipeline.Abstractions;

public interface IGraphParser
{
    DataPipelineGraph ParseGraph(GraphDto graphDto);
}