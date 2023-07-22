using ETLProject.Common.Table;

namespace ETLProject.Contract.DbWriter;

public class DbWriterParameter
{
    public BulkConfiguration BulkConfiguration { get; set; }
    public TableType TableType { get; set; }
    public string NewTableName { get; set; }
    public Guid DestinationConnectionId { get; set; }
}