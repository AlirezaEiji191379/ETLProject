
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


var sqlServerMetaDataBusiness = dbConnectionMetaDataProvider.GetMetaDataBusiness(DataSourceType.SQLServer);

var result = await sqlServerMetaDataBusiness.GetDatabases(new ConnectionDto()
{
    DataSourceType = DataSourceType.SQLServer,
    Host = "192.168.30.5",
    Password = "!@#123qwe",
    Port = "1433",
    Username = "sa"
});

Console.WriteLine();


