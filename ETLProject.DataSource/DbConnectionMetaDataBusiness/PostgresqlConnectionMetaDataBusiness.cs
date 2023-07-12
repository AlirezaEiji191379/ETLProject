﻿using ETLProject.Common.Database;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;

namespace ETLProject.DataSource.DbConnectionMetaDataBusiness;

internal class PostgresqlConnectionMetaDataBusiness : IDbConnectionMetaDataBusiness
{
    public DataSourceType DataSourceType { get; } = DataSourceType.Postgresql;
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