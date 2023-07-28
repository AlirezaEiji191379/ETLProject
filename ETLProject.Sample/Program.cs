using ETLProject.Common.Common.DIManager;
using ETLProject.Common.Table;
using ETLProject.Contract.Where.Conditions;
using ETLProject.Contract.Where.Enums;
using ETLProject.DataSource.Common.DIManager;
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

var conditionBuilder = provider.GetService<IWhereQueryBusiness>();

var query = new Query("Users").Select("Id", "FullName", "Age");

var refinedQuery = conditionBuilder.AddWhereCondition(new ETLTable()
    {
        Query = new Query("Users").Select("Id", "FullName", "Age"),
        AliasName = "t"
    }
    ,
    new LogicalCondition()
    {
        LogicalOperator = LogicalOperator.Or,
        ChildConditions = new List<Condition>()
        {
            new FieldCondition()
            {
                ColumnName = "Age",
                ConditionType = ConditionType.GreaterThan,
                Value = 18
            },
            new FieldCondition()
            {
                ColumnName = "Id",
                ConditionType = ConditionType.Equals,
                Value = 12
            },
            new LogicalCondition()
            {
                LogicalOperator = LogicalOperator.And,
                ChildConditions = new List<Condition>()
                {
                    new FieldCondition()
                    {
                        ColumnName = "FullName",
                        ConditionType = ConditionType.Equals,
                        Value = "reza"
                    },
                    new FieldCondition()
                    {
                        ColumnName = "Id",
                        ConditionType = ConditionType.GreaterThan,
                        Value = 14
                    }
                }
            }
        }
    });


var compiler = new SqlServerCompiler();

Console.WriteLine(compiler.Compile(refinedQuery.Query));