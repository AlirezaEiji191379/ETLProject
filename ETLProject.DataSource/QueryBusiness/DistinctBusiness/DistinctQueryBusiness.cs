using ETLProject.Common.Table;
using ETLProject.DataSource.QueryBusiness.DistinctBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.DistinctBusiness;

public class DistinctQueryBusiness : IDistinctQueryBusiness
{
    public ETLTable DistinctTable(ETLTable inputTable)
    {
        inputTable.Query.Distinct();
        return inputTable;
    }
}