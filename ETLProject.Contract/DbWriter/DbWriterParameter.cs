using ETLProject.Common.Table;

namespace ETLProject.Contract.DbWriter;

public class DbWriterParameter
{
    public BulkConfiguration BulkConfiguration { get; set; }
    public TableType TableType { get; set; }
    public PermanentTableParameter PermanentTableParameter { get; set; }
}