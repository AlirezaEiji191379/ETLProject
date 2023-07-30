using System.Text;
using ETLProject.Common.Abstractions;
using ETLProject.Common.Table;
using ETLProject.Contract.Join;
using ETLProject.Contract.Join.Enums;
using ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;
using SqlKata;

namespace ETLProject.DataSource.QueryBusiness.JoinBusiness.QueryMaker;

internal class JoinQueryMaker : IJoinQueryMaker
{
    private readonly IRandomStringGenerator _randomStringGenerator;

    public JoinQueryMaker(IRandomStringGenerator randomStringGenerator)
    {
        _randomStringGenerator = randomStringGenerator;
    }

    public void AddJoinQueryToResultTable(ETLTable leftTable, ETLTable rightTable, ETLTable resultTable,
        JoinParameter joinParameter)
    {
        var leftAlias = _randomStringGenerator.GenerateRandomString(8);
        var rightAlias = _randomStringGenerator.GenerateRandomString(8);

        var joinSelectColumns = joinParameter
            .LeftTableSelectedColumns
            .Select(leftTableSelectedColumn => PrepareColumnFullName(leftAlias, leftTableSelectedColumn.ColumnName, leftTableSelectedColumn.OutputTitle))
            .ToList();
        
        joinSelectColumns.AddRange(joinParameter.RigthTableSelectedColumns.Select(x => PrepareColumnFullName(rightAlias,x.ColumnName,x.OutputTitle))); 
        
        var firstJoinFullColumnName = PrepareColumnFullName(leftAlias,joinParameter.LeftTableJoinColumnName,string.Empty);
        var secondJoinFullColumnName = PrepareColumnFullName(rightAlias,joinParameter.RigthTableJoinColumnName,string.Empty);


        var resultTableSelectColumns =
            resultTable.Columns.Select(x => resultTable.GetColumnFullNameById(x.EtlColumnId));
        
        var joinQuery = ApplyJoin(rightTable, leftTable,joinParameter, leftAlias, rightAlias, firstJoinFullColumnName, secondJoinFullColumnName);
        joinQuery.Select(joinSelectColumns);
        resultTable.Query = new Query().From(joinQuery, resultTable.AliasName).Select(resultTableSelectColumns);
    }

    private static Query ApplyJoin(
        ETLTable rightTable,
        ETLTable leftTable,
        JoinParameter joinParameter,
        string leftAlias, 
        string rightAlias,
        string firstJoinFullColumnName,
        string secondJoinFullColumnName)
    {
        var joinQuery = new Query().From(leftTable.Query, leftAlias);
        if (joinParameter.JoinType == JoinType.Left)
        {
            joinQuery.LeftJoin(rightTable.Query.As(rightAlias),
                j => j.On(firstJoinFullColumnName, secondJoinFullColumnName));
        }
        else if (joinParameter.JoinType == JoinType.Right)
        {
            joinQuery.RightJoin(rightTable.Query.As(rightAlias),
                j => j.On(firstJoinFullColumnName, secondJoinFullColumnName));
        }
        else
        {
            joinQuery.Join(rightTable.Query.As(rightAlias),
                j => j.On(firstJoinFullColumnName, secondJoinFullColumnName));
        }
        return joinQuery;
    }


    private string PrepareColumnFullName(string tableAliasName, string columnName, string columnAliasName)
    {
        if (tableAliasName == null) throw new ArgumentNullException(nameof(tableAliasName));
        if (columnName == null) throw new ArgumentNullException(nameof(columnName));
        var result = new StringBuilder(tableAliasName).Append('.').Append(columnName);
        if (!string.IsNullOrEmpty(columnAliasName))
        {
            return result.Append(" as ").Append(columnAliasName).ToString();
        }
        return result.ToString();
    }
}