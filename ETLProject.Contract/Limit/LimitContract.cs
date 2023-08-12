using ETLProject.Common.PipeLine.Abstractions;

namespace ETLProject.Contract.Limit;

public class LimitContract : IPluginConfig
{
    public int Top { get; init; }
}