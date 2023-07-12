using ETLProject.Common.Table;

namespace ETLProject.DataSource.QueryBusiness.DistinctBusiness.Abstractions;

public interface IDistinctQueryBusiness
{
    ETLTable DistinctTable(ETLTable inputTable);
}