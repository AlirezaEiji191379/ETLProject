using ETLProject.Common.Database;
using ETLProject.DataSource.DbTransfer;
using ETLProject.Infrastructure.Entities;

namespace ETLProject.DataSource.Abstractions;

internal interface IDataTransferDecisionMaker
{
    DataTransferDecision MakeDecision(EtlConnection etlConnection,DatabaseConnectionParameters databaseConnectionParameters);
}