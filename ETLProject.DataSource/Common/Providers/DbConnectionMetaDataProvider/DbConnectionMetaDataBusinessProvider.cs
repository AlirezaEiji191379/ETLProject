using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;

namespace ETLProject.DataSource.Common.Providers.DbConnectionMetaDataProvider;

internal class DbConnectionMetaDataBusinessProvider : IDbConnectionMetaDataBusinessProvider
{
    private readonly Dictionary<DataSourceType, IDbConnectionMetaDataBusiness> metaDataBusinessesByDataSourceType;

    public DbConnectionMetaDataBusinessProvider(IEnumerable<IDbConnectionMetaDataBusiness> dbConnectionMetaDataBusinesses)
    {
        metaDataBusinessesByDataSourceType = dbConnectionMetaDataBusinesses.ToDictionary(x => x.DataSourceType);
    }

    public IDbConnectionMetaDataBusiness GetMetaDataBusiness(DataSourceType dataSourceType)
    {
        return metaDataBusinessesByDataSourceType[dataSourceType];
    }
}