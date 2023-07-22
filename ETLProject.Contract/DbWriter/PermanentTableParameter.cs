namespace ETLProject.Contract.DbWriter;

public class PermanentTableParameter
{
    public string TableName { get; set; }
    public List<TableWriterColumnParameter> TableWriterColumnParameters { get; set; }
}