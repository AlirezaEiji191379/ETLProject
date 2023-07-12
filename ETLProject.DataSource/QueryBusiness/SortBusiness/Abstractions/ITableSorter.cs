using ETLProject.Common.Table;
using ETLProject.Contract.Sort;

namespace ETLProject.DataSource.QueryBusiness.SortBusiness.Abstractions
{
    public interface ITableSorter
    {
        ETLTable SortTable(ETLTable inputTable,SortContract sortContract);
    }
}
