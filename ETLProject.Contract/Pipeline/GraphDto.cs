using MediatR;

namespace ETLProject.Contract.Pipeline;

public class GraphDto : IRequest<ResponseDto>
{
    public string RunningPlugin { get; set; }
    public List<EdgeDto> Edges { get; set; }
    public Dictionary<string,PluginConfigDto> PluginConfigs { get; set; }
}