using ETLProject.Common.Database;
using ETLProject.Contract.DbConnectionContracts;

namespace ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;

public interface IDbConnectionMetaDataBusiness
{
    DataSourceType DataSourceType { get;}
    Task<List<string>> GetDatabases(ConnectionDto connectionDto);
    Task<List<DbTableAttributes>> GetDatabaseTables(ConnectionDto connectionDto);
    Task<List<DbTableColumnInformation>> GetTableColumns(ConnectionDto connectionDto,string tableName);
    void TestConnection(ConnectionDto connectionDto);
}