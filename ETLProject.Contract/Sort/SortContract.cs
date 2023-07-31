using ETLProject.Common.PipeLine.Abstractions;

namespace ETLProject.Contract.Sort
{
    public class SortContract : IPluginConfig
    {
        public List<OrderColumnDto> Columns { get; set; }
    }
}
