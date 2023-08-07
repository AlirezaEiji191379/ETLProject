namespace ETLProject.Contract.Pipeline;

public class GraphDto
{
    public List<EdgeDto> Edges { get; set; }
    public Dictionary<string,PluginConfigDto> PluginConfigs { get; set; }
}