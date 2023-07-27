using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbTransfer.Configs;
using Microsoft.EntityFrameworkCore;
using SqlKata;
using SqlKata.Compilers.Abstractions;

namespace ETLProject.DataSource.DbTransfer.Strategies;

internal class AmongOneServerInsert : IDataTransferStrategy
{
    private readonly IQueryFactoryProvider _queryFactoryProvider;

    public AmongOneServerInsert(IQueryFactoryProvider queryFactoryProvider)
    {
        _queryFactoryProvider = queryFactoryProvider;
    }

    public DataTransferType DataTransferType => DataTransferType.AmongOneConnection;
    public DataTransferAction DataTransferAction => DataTransferAction.Insert;

    public async Task TransferData(DataTransferParameter dataTransferParameter)
    {
        var insertIntoQuery = new Query(dataTransferParameter.DestinationTable.TableName)
            .AsInsert(new List<string>(), dataTransferParameter.SourceTable.Query);
        var queryFactory =
            _queryFactoryProvider.GetQueryFactoryByConnection(dataTransferParameter.SourceTable.DatabaseConnection);
        await queryFactory.ExecuteAsync(insertIntoQuery);
    }
}