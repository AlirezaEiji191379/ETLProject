﻿using ETLProject.Common.Table;
using ETLProject.DataSource.QueryManager.Common;
using System.Data;

namespace ETLProject.DataSource.Query.Abstractions
{
    public interface IDataBaseBulkReader : IDisposable
    {
        DataTable ReadDataInBulk(ETLTable etlTable,BulkConfiguration bulk);
    }
}