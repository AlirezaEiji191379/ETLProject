using ETLProject.Common.Database;
using ETLProject.Common.PipeLine.Abstractions;

namespace ETLProject.Contract.DBReader;

public class DbReaderContract : IPluginConfig
{
    public string TableName { get; set; }
    public List<DbColumnDto> SelectedColumns { get; set; }
    public DataSourceType DataSourceType { get; set; }
    public DatabaseConnectionParameters DatabaseConnectionParameters { get; set; }
    public string Schema { get; set; }
}