using ETLProject.Common.Table;
using ETLProject.Contract.Aggregate;

namespace ETLProject.DataSource.QueryBusiness.AggregateBusiness.Abstractions;

public interface IAggregateQueryBusiness
{
    ETLTable AddAggregation(ETLTable inputTable, AggregationParameter aggregationParameter);
}