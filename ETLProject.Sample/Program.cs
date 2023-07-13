
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


var sqlServerMetaDataBusiness = dbConnectionMetaDataProvider.GetMetaDataBusiness(DataSourceType.MySql);

var result = await sqlServerMetaDataBusiness.GetTableColumns(new ConnectionDto()
{
    DataSourceType = DataSourceType.MySql,
    Host = "localhost",
    Password = "92?VH2WMrx",
    Port = "3306",
    Username = "alirezaeiji"
},"kheft","user");

Console.WriteLine();


