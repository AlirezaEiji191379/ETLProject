using ETLProject.Common.Table;

namespace ETLProject.Contract.DBReader;

public class DbColumnDto
{
    public string Name { get; set; }
    public ColumnType ColumnType { get; set; }
    public int? Length { get; set; }
    public int? Precision { get; set; }
    public string Collation { get; set; }
}