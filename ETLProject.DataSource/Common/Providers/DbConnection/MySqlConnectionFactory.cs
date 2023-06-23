﻿using ETLProject.Common.Database;
using ETLProject.DataSource.Query.Abstractions;
using MySql.Data.MySqlClient;
using System.Data;

namespace ETLProject.DataSource.QueryManager.Common.Providers.DbConnection
{
    internal class MySqlConnectionFactory : IDbConnectionFactory
    {
        public DataSourceType DataSourceType { get; } = DataSourceType.MySql;
        public IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection)
        {
            var connectionString = $"Server={databaseConnection.Host};Database={databaseConnection.DatabaseName};Uid={databaseConnection.Username};Pwd={databaseConnection.Password};";
            return new MySqlConnection(connectionString);
        }
    }
}