using ETLProject.Common.Database;
using ETLProject.DataSource.DbConnectionMetaDataBusiness.Abstractions;

namespace ETLProject.DataSource.Abstractions;

public interface IDbConnectionMetaDataBusinessProvider
{
    IDbConnectionMetaDataBusiness GetMetaDataBusiness(DataSourceType dataSourceType);
}