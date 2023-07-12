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

        public BaseDBColumn AdaptType(ETLColumnType etlColumnType)
        {
            switch (etlColumnType.Type)
            {
                case ColumnType.StringType:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.Varchar,
                        Length = etlColumnType.Length ?? 200
                    };
                case ColumnType.DoubleType:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.Double,
                        Length = etlColumnType.Length ?? 30,
                        Precision = etlColumnType.Precision ?? 10,
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
                case ColumnType.DateTime:
                    return new MySqlDBColumn()
                    {
                        MySqlDbType = MySqlDbType.DateTime
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
