﻿using ETLProject.Common.Table;
using ETLProject.DataSource.Common;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDataBaseBulkReader
    {
        IAsyncEnumerable<DataTable> ReadDataInBulk(ETLTable etlTable, BulkConfiguration bulk);
    }
}
