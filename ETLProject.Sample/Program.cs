using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.Aggregate;
using ETLProject.Contract.Aggregate.Enums;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.Join;
using ETLProject.Contract.Join.Enums;
using ETLProject.Contract.Where.Conditions;
using ETLProject.Contract.Where.Enums;
using ETLProject.DataSource.Common.DIManager;
using ETLProject.DataSource.QueryBusiness.AggregateBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.WhereQueryBusiness.Abstractions;
using ETLProject.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SqlKata;
using SqlKata.Compilers;

var serviceCollection = new ServiceCollection();

serviceCollection.AddCommonServices();
serviceCollection.AddDataSourceQueryServices();
serviceCollection.AddInfrastructureServices();

var provider = serviceCollection.BuildServiceProvider();
var joinBusiness = provider.GetService<IJoinQueryBusiness>();

var leftTable = new ETLTable()
{
    Columns = new List<ETLColumn>()
    {
        new()
        {
            Name = "id",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.Int32Type
            }
        },
        new()
        {
            Name = "fullname",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.StringType,
                Length = 200
            }
        },
        new()
        {
            Name = "city",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.StringType,
                Length = 200
            }
        }
    },
    TableName = "Users",
    TableSchema = "dbo",
    AliasName = "t",
    TableType = TableType.Permanent,
    DataSourceType = DataSourceType.SQLServer,
    DatabaseConnection = new DatabaseConnectionParameters()
    {
        DataSourceType = DataSourceType.SQLServer,
        Host = "localhost",
        Port = "1433",
        DatabaseName = "testDb",
        Username = "sa",
        Password = "92?VH2WMrx"
    }
};
var rightTable = new ETLTable()
{
    Columns = new List<ETLColumn>()
    {
        new()
        {
            Name = "Id",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.Int32Type
            }
        },
        new()
        {
            Name = "FullName",
            ETLColumnType = new ETLColumnType()
            {
                Type = ColumnType.StringType,
                Length = 200
            }
        }
    },
    TableName = "PostgresUser",
    TableSchema = "dbo",
    AliasName = "x",
    TableType = TableType.Permanent,
    DataSourceType = DataSourceType.SQLServer,
    DatabaseConnection = new DatabaseConnectionParameters()
    {
        DataSourceType = DataSourceType.SQLServer,
        Host = "localhost",
        Port = "1433",
        DatabaseName = "testDb",
        Username = "sa",
        Password = "92?VH2WMrx"
    }
};

var joinParam = new JoinParameter()
{
    JoinType = JoinType.Inner,
    UseLeftTableConnection = true,
    BulkConfiguration = new BulkConfiguration(){BatchSize = 100},
    LeftTableJoinColumnName = "id",
    RigthTableJoinColumnName = "Id",
    LeftTableSelectedColumns = new List<JoinColumnParameter>()
    {
        new ()
        {
            ColumnName = "id",
            OutputTitle = "IdOfCityTable"
        },
        new()
        {
            ColumnName = "city",
            OutputTitle = "city"
        }

    },
    RigthTableSelectedColumns = new List<JoinColumnParameter>()
    {
        new()
        {
            ColumnName = "FullName",
            OutputTitle = "FullNameOfRight"
        }
    }
};

var etlTable = await joinBusiness.JoinTables(leftTable,rightTable,joinParam);

var compiler = new SqlServerCompiler();

Console.WriteLine(compiler.Compile(etlTable.Query));