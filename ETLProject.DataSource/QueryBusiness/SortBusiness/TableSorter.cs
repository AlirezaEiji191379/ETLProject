﻿using ETLProject.Common.Table;
using ETLProject.Contract.Sort;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.SortBusiness.Exceptions;

namespace ETLProject.DataSource.QueryBusiness.SortBusiness
{
    internal class TableSorter : ITableSorter
    {
        public ETLTable SortTable(ETLTable inputTable, SortContract sortContract)
        {
            foreach (var orderColumnDto in sortContract.Columns)
            {
                var etlColumn = inputTable.Columns.FirstOrDefault(column => column.Name == orderColumnDto.Name);
                if (etlColumn == null)
                    throw new ColumnDoesNotExistException(orderColumnDto.Name);
                var columnFullName = inputTable.GetColumnFullNameById(etlColumn.EtlColumnId);
                if (orderColumnDto.SortType == SortType.Descending)
                    inputTable.Query.OrderByDesc(columnFullName);
                else
                {
                    inputTable.Query.OrderBy(columnFullName);
                }
            }
            return inputTable;
        }
    }
}
