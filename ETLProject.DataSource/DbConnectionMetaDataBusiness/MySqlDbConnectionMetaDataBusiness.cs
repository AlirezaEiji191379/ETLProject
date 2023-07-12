using ETLProject.Common.Database;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;

namespace ETLProject.DataSource.DbConnectionMetaDataBusiness;

internal class MySqlDbConnectionMetaDataBusiness : IDbConnectionMetaDataBusiness
{
    public DataSourceType DataSourceType { get; }
    public Task<List<string>> GetDatabases(ConnectionDto connectionDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<DbTableAttributes>> GetDatabaseTables(ConnectionDto connectionDto, string databaseName)
    {
        throw new NotImplementedException();
    }

    public Task<List<DbTableColumnInformation>> GetTableColumns(ConnectionDto connectionDto, string databaseName, string tableName)
    {
        throw new NotImplementedException();
    }
}