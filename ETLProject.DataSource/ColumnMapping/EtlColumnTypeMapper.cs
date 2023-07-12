using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata.DbTypes.DbColumn;
using SqlKata.DbTypes.Enums;

namespace ETLProject.DataSource.ColumnMapping;

internal class EtlColumnTypeMapper : IEtlColumnTypeMapper
{
    public ETLColumnType AdaptSqlServerType(SqlServerDBColumn sqlServerDbColumn)
    {
        switch (sqlServerDbColumn.SqlServerDbType)
        {
            case SqlServerDbType.Varchar:
            case SqlServerDbType.Nvarchar:
            case SqlServerDbType.Char:
            case SqlServerDbType.NChar:
            case SqlServerDbType.Text:
                return new ETLColumnType()
                {
                    Length = sqlServerDbColumn.Length,
                    Precision = sqlServerDbColumn.Precision,
                    Type = ColumnType.StringType
                };
            
            case SqlServerDbType.Int:
            case SqlServerDbType.SmallInt:
            case SqlServerDbType.TinyInt:
                return new ETLColumnType()
                {
                    Type = ColumnType.Int32Type
                };
            
            case SqlServerDbType.BigInt:
                return new ETLColumnType()
                {
                    Type = ColumnType.LongType
                };
            
            case SqlServerDbType.Decimal:
            case SqlServerDbType.Float:
            case SqlServerDbType.Real:
            case SqlServerDbType.numeric:
            case SqlServerDbType.money:
                return new ETLColumnType()
                {
                    Type = ColumnType.DoubleType,
                    Precision = sqlServerDbColumn.Precision,
                    Length = sqlServerDbColumn.Length
                };
            case SqlServerDbType.Bit:
                return new ETLColumnType()
                {
                    Type = ColumnType.BooleanType
                };
            case SqlServerDbType.UniqueIdentifier:
                return new ETLColumnType()
                {
                    Type = ColumnType.Guid
                };
            case SqlServerDbType.DateTime:
            case SqlServerDbType.DateTime2:
                return new ETLColumnType()
                {
                    Type = ColumnType.DateTime
                };
            default:
                throw new NotSupportedException();
        }
    }

    public ETLColumnType AdaptPostgresqlType(PostgresqlDBColumn postgresqlDbColumn)
    {
        switch (postgresqlDbColumn.PostgresqlDbType)
        {
            case PostgresqlDbType.Integer:
            case PostgresqlDbType.Serial:
            case PostgresqlDbType.Smallint:
            case PostgresqlDbType.Bigserial:
            case PostgresqlDbType.Smallserial:
                return new ETLColumnType()
                {
                    Type = ColumnType.Int32Type
                };
            
            case PostgresqlDbType.Bigint:
                return new ETLColumnType()
                {
                    Type = ColumnType.LongType
                };
            
            case PostgresqlDbType.Numeric:
            case PostgresqlDbType.Double_precision:
            case PostgresqlDbType.Real:
                return new ETLColumnType()
                {
                    Type = ColumnType.DoubleType,
                    Precision = postgresqlDbColumn.Precision,
                    Length = postgresqlDbColumn.Length
                };
            case PostgresqlDbType.Boolean:
                return new ETLColumnType()
                {
                    Type = ColumnType.BooleanType
                };
            case PostgresqlDbType.Uuid:
                return new ETLColumnType()
                {
                    Type = ColumnType.Guid
                };
            case PostgresqlDbType.Timestamp_with_time_zone:
            case PostgresqlDbType.Timestamp_without_time_zone:
                return new ETLColumnType()
                {
                    Type = ColumnType.DateTime
                };
            case PostgresqlDbType.Char:
            case PostgresqlDbType.Character_varying:
            case PostgresqlDbType.Character:
            case PostgresqlDbType.Text:
                return new ETLColumnType()
                {
                    Type = ColumnType.StringType,
                    Length = postgresqlDbColumn.Length
                };
            default:
                throw new NotImplementedException();
        }
    }

    public ETLColumnType AdaptMySqlType(MySqlDBColumn mysqlDbColumn)
    {
        switch (mysqlDbColumn.MySqlDbType)
        {
            case MySqlDbType.Int:
            case MySqlDbType.BigInt:
            case MySqlDbType.MediumInt:
            case MySqlDbType.SmallInt:
            case MySqlDbType.TinyInt:
                return new ETLColumnType()
                {
                    Type = ColumnType.Int32Type
                };
            case MySqlDbType.Decimal:
            case MySqlDbType.Float:
            case MySqlDbType.Double:
                return new ETLColumnType()
                {
                    Precision = mysqlDbColumn.Precision,
                    Length = mysqlDbColumn.Length,
                    Type = ColumnType.DoubleType
                };

            case MySqlDbType.Bit:
                return new ETLColumnType()
                {
                    Type = ColumnType.BooleanType
                };
            
            case MySqlDbType.DateTime:
            case MySqlDbType.TimeStamp:
                return new ETLColumnType()
                {
                    Type = ColumnType.DateTime
                };
            case MySqlDbType.Varchar:
            case MySqlDbType.Char:
            case MySqlDbType.LongText:
            case MySqlDbType.Text:
            case MySqlDbType.TinyText:
            case MySqlDbType.MediumText:
                return new ETLColumnType()
                {
                    Length = mysqlDbColumn.Length,
                    Type = ColumnType.StringType
                };
            default:
                throw new NotSupportedException();       
        }
    }
}