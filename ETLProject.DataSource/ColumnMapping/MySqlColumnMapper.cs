using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata.DbTypes.DbColumn;
using SqlKata.DbTypes.Enums;

namespace ETLProject.DataSource.ColumnMapping
{
    internal class MySqlColumnMapper : IColumnTypeMapper
    {
        public DataSourceType DataSourceType => DataSourceType.MySql;

        public BaseDBColumn AdaptType(ETLColumn etlColumn)
        {
            switch (etlColumn.ETLColumnType.Type)
            {
                case ColumnType.StringType:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.Varchar,
                        Length = etlColumn.ETLColumnType.Length ?? 200
                    };
                case ColumnType.DoubleType:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.Double,
                        Length = etlColumn.ETLColumnType.Length ?? 30,
                        Precision = etlColumn.ETLColumnType.Precision ?? 10,
                    };
                case ColumnType.BooleanType:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.TinyInt,
                    };
                case ColumnType.Int32Type:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.Int,
                    };
                case ColumnType.LongType:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.BigInt,
                    };
                case ColumnType.Guid:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.Char,
                        Length = 16
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
