using SqlKata.DbTypes.Enums;

namespace ETLProject.DataSource.ColumnMapping;

public static class PostgresqlEnumTypeMapper
{
    public static Dictionary<string, PostgresqlDbType> PostgreSqlDataTypes = new Dictionary<string, PostgresqlDbType>();
    public static Dictionary<PostgresqlDbType, string> PostgreSqlEnumTypes = new Dictionary<PostgresqlDbType, string>();

    static PostgresqlEnumTypeMapper()
    {
        PostgreSqlDataTypes.Add("character", PostgresqlDbType.Character);
        PostgreSqlDataTypes.Add("character varying", PostgresqlDbType.Character_varying);
        PostgreSqlDataTypes.Add("text", PostgresqlDbType.Text);
        PostgreSqlDataTypes.Add("\"char\"", PostgresqlDbType.Char);

        PostgreSqlEnumTypes.Add(PostgresqlDbType.Character, "character");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Character_varying, "character varying");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Text, "text");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Char, "\"char\"");
        //int
        PostgreSqlDataTypes.Add("smallint", PostgresqlDbType.Smallint);
        PostgreSqlDataTypes.Add("integer", PostgresqlDbType.Integer);
        PostgreSqlDataTypes.Add("bigint", PostgresqlDbType.Bigint);

        PostgreSqlEnumTypes.Add(PostgresqlDbType.Smallint, "smallint");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Integer, "integer");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Bigint, "bigint");
        //float
        PostgreSqlDataTypes.Add("real", PostgresqlDbType.Real);
        PostgreSqlDataTypes.Add("numeric", PostgresqlDbType.Numeric);
        PostgreSqlDataTypes.Add("double precision", PostgresqlDbType.Double_precision);

        PostgreSqlEnumTypes.Add(PostgresqlDbType.Real, "real");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Numeric, "numeric");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Double_precision, "double precision");
        //for identity keys
        PostgreSqlDataTypes.Add("smallserial", PostgresqlDbType.Smallserial);
        PostgreSqlDataTypes.Add("serial", PostgresqlDbType.Serial);
        PostgreSqlDataTypes.Add("bigserial", PostgresqlDbType.Bigserial);
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Smallserial, "smallserial");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Serial, "serial");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Bigserial, "bigserial");


        //boolean data types
        PostgreSqlDataTypes.Add("boolean", PostgresqlDbType.Boolean);
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Boolean, "boolean");
        //date
        PostgreSqlDataTypes.Add("date", PostgresqlDbType.Date);
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Date, "date");
        //datetime
        PostgreSqlDataTypes.Add("timestamp with time zone", PostgresqlDbType.Timestamp_with_time_zone);
        PostgreSqlDataTypes.Add("timestamp without time zone", PostgresqlDbType.Timestamp_without_time_zone);
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Timestamp_with_time_zone, "timestamp with time zone");
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Timestamp_without_time_zone, "timestamp without time zone");
        // guid
        PostgreSqlDataTypes.Add("uuid", PostgresqlDbType.Uuid);
        PostgreSqlEnumTypes.Add(PostgresqlDbType.Uuid, "uuid");
    }
}