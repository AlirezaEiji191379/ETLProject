using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata.DbTypes.DbColumn;
using SqlKata.DbTypes.Enums;

namespace ETLProject.DataSource.ColumnMapping
{
    internal class PostgresqlColumnMapper : IColumnTypeMapper
    {
        public DataSourceType DataSourceType => DataSourceType.Postgresql;

        public BaseDBColumn AdaptType(ETLColumn etlColumn)
        {
            switch (etlColumn.ETLColumnType.Type)
            {
                case ColumnType.StringType:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Character_varying,
                        Length = etlColumn.ETLColumnType.Length ?? 200
                    };
                case ColumnType.DoubleType:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Numeric,
                        Length = etlColumn.ETLColumnType.Length ?? 30,
                        Precision = etlColumn.ETLColumnType.Precision ?? 10,
                    };
                case ColumnType.BooleanType:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Boolean,
                    };
                case ColumnType.Int32Type:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Integer,
                    };
                case ColumnType.LongType:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Bigint,
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
