namespace ETLProject.Contract.DbWriter;

public class TableWriterColumnParameter
{
    public TableWriterColumnParameter()
    {
        ColumnId = Guid.NewGuid();
    }
    public bool IsNullable { get; set; }
    public bool IsUnique { get; set; }
    public bool IsPrimaryKey { get; set; }
    public string ColumnName { get; set; }
    public Guid ColumnId { get; set; }
}