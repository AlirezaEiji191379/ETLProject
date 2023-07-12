using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata.DbTypes.DbColumn;
using SqlKata.DbTypes.Enums;

namespace ETLProject.DataSource.ColumnMapping
{
    internal class SqlServerColumnMapper : IColumnTypeMapper
    {
        public DataSourceType DataSourceType => DataSourceType.SQLServer;

        public BaseDBColumn AdaptType(ETLColumnType etlColumnType)
        {
            switch (etlColumnType.Type)
            {
                case ColumnType.StringType:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType = SqlServerDbType.Varchar,
                        Length = etlColumnType.Length ?? 200 
                    };
                case ColumnType.DoubleType:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType= SqlServerDbType.Decimal,
                        Length = etlColumnType.Length ?? 30,
                        Precision = etlColumnType.Precision ?? 10,
                    };
                case ColumnType.BooleanType:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType = SqlServerDbType.TinyInt,
                    };
                case ColumnType.Int32Type:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType= SqlServerDbType.Int,
                    };
                case ColumnType.LongType:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType = SqlServerDbType.BigInt,
                    };
                case ColumnType.Guid:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType = SqlServerDbType.UniqueIdentifier
                    };
                case ColumnType.DateTime:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType = SqlServerDbType.DateTime
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
