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

        public BaseDBColumn AdaptType(ETLColumn etlColumn)
        {
            switch (etlColumn.ETLColumnType.Type)
            {
                case ColumnType.StringType:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType = SqlServerDbType.Varchar,
                        Length = etlColumn.ETLColumnType.Length ?? 200 
                    };
                case ColumnType.DoubleType:
                    return new SqlServerDBColumn()
                    {
                        SqlServerDbType= SqlServerDbType.Decimal,
                        Length = etlColumn.ETLColumnType.Length ?? 30,
                        Precision = etlColumn.ETLColumnType.Precision ?? 10,
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
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
