using ETLProject.Common.Database;
using ETLProject.Common.Table;
using SqlKata.DbTypes.DbColumn;

namespace ETLProject.DataSource.Abstractions
{
    public interface IColumnTypeMapper
    {
        DataSourceType DataSourceType { get; }
        BaseDBColumn AdaptType(ETLColumnType etlColumnType);
    }
}
