using ETLProject.Common.Table;

namespace ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;

public interface IWhereQueryBusiness
{
    ETLTable AddWhereCondition(ETLTable inputTable);
}