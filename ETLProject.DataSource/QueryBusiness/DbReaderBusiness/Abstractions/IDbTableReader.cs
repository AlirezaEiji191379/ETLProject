using ETLProject.Common.Table;
using ETLProject.Contract.DBReader;

namespace ETLProject.DataSource.QueryBusiness.DbReaderBusiness.Abstractions;

public interface IDbTableReader
{
    ETLTable ReadTable(DbReaderContract dbReaderContract);
}