namespace ETLProject.Contract.DbConnectionContracts;

public class DbTableColumnInformation
{
    public string ColumnName { get; init; }
    public string DatabaseType { get; init; }
    public string SystemType { get; init; }
    public int? Length { get; init; }
    public int? Precision { get; init; }
    public string Collation { get; init; }
}