using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;

namespace ETLProject.Pipeline.Abstractions;

public interface IPluginRunnerFactory
{
    IPluginRunner GetPluginRunner(PluginType pluginType,IPluginConfig pluginConfig);
}