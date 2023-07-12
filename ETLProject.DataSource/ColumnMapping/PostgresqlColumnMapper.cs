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

        public BaseDBColumn AdaptType(ETLColumnType etlColumnType)
        {
            switch (etlColumnType.Type)
            {
                case ColumnType.StringType:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Character_varying,
                        Length = etlColumnType.Length ?? 200
                    };
                case ColumnType.DoubleType:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Numeric,
                        Length = etlColumnType.Length ?? 30,
                        Precision = etlColumnType.Precision ?? 10,
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
                case ColumnType.Guid:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Uuid
                    };
                case ColumnType.DateTime:
                    return new PostgresqlDBColumn()
                    {
                        PostgresqlDbType = PostgresqlDbType.Timestamp_without_time_zone
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
