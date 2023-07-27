using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;

namespace ETLProject.DataSource.DbTransfer.Configs;

public class DataTransferParameter
{
    public ETLTable SourceTable { get; set; }
    public ETLTable DestinationTable { get; set; }
    public BulkConfiguration BulkConfiguration { get; set; }
    public DataTransferAction DataTransferAction { get; set; }
    
}