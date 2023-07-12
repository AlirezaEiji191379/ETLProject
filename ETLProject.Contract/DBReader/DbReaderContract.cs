using ETLProject.Common.Database;

namespace ETLProject.Contract.DBReader;

public class DbReaderContract
{
    public string TableName { get; set; }
    public List<DbColumnDto> SelectedColumns { get; set; }
    public DataSourceType DataSourceType { get; set; }
    public DatabaseConnectionParameters DatabaseConnectionParameters { get; set; }
}