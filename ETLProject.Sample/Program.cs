
using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.DIManager;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddCommonServices();
serviceCollection.AddDataSourceQueryServices();

var provider = serviceCollection.BuildServiceProvider();

var dbConnectionMetaDataProvider = provider.GetRequiredService<IDbConnectionMetaDataBusinessProvider>();


var sqlServerMetaDataBusiness = dbConnectionMetaDataProvider.GetMetaDataBusiness(DataSourceType.Postgresql);

var result = await sqlServerMetaDataBusiness.GetTableColumns(new ConnectionDto()
{
    DataSourceType = DataSourceType.Postgresql,
    Host = "192.168.30.202",
    Password = "!@#123qwe",
    Port = "5432",
    Username = "postgres"
},"Star_24.0.0.0_AccessManagement_Ammii","BlockedUserLoginHistories");

Console.WriteLine();


