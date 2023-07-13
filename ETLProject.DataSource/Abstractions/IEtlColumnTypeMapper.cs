using ETLProject.Common.Table;
using SqlKata.DbTypes.DbColumn;

namespace ETLProject.DataSource.Abstractions;

internal interface IEtlColumnTypeMapper
{
    ETLColumnType AdaptSqlServerType(SqlServerDBColumn sqlServerDbColumn);
    ETLColumnType AdaptPostgresqlType(PostgresqlDBColumn postgresqlDbColumn);
    ETLColumnType AdaptMySqlType(MySqlDBColumn mysqlDbColumn);
}