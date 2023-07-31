using ETLProject.Common.PipeLine.Abstractions;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter.Enums;

namespace ETLProject.Contract.DbWriter;

public class DbWriterParameter : IPluginConfig
{
    public BulkConfiguration BulkConfiguration { get; set; }
    public string DestinationTableName { get; set; }
    public bool UseInputConnection { get; set; }
    public Guid? DestinationConnectionId { get; set; }
    public DbTransferAction DbTransferAction { get; set; }
    public string DestinationTableSchema { get; set; }
}