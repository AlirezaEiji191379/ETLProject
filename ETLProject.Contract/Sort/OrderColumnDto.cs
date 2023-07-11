using ETLProject.Common.Table;

namespace ETLProject.Contract.Sort
{
    public class OrderColumnDto
    {
        public string Name { get; set; }
        public ColumnType ColumnType { get; set; }
        public SortType SortType { get; set; } 
    }
}
