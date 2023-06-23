﻿using ETLProject.Common.Database;

namespace ETLProject.Common.Table
{
    public class ETLTable
    {
        public DataSourceType DataSourceType { get; set; }
        public TableType TableType { get; set; }
        public List<Column> Columns { get; set; }
        public string TableName { get; set; }
        public DatabaseConnectionParameters DatabaseConnection { get; set; }
    }
}
