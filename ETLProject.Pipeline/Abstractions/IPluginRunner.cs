using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.PipeLine.Enums;
using ETLProject.Common.Table;

namespace ETLProject.Pipeline.Abstractions;

public interface IPluginRunner
{
    void AddInputEtlTable(ETLTable etlTable);
    PluginType PluginType { get; }
    Task<ETLTable> RunPlugin();
}