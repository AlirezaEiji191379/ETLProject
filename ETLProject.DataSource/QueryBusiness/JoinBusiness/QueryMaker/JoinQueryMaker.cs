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

        var selectColumns = leftTable
            .Columns
            .Where(x => joinParameter.LeftTableSelectedColumnNames.Contains(x.Name))
            .Select(x => PrepareColumnFullName(leftAlias, x.Name))
            .ToList();
        var rightSelectColumns = rightTable
            .Columns
            .Where(x => joinParameter.RigthTableSelectedColumnNames.Contains(x.Name))
            .Select(x => PrepareColumnFullName(rightAlias, x.Name));

        selectColumns.AddRange(rightSelectColumns);
        
        var firstJoinFullColumnName = leftTable
            .Columns
            .Where(x => x.Name == joinParameter.LeftTableJoinColumnName)
            .Select(x => PrepareColumnFullName(leftAlias, x.Name))
            .First();

        var secondJoinFullColumnName = rightTable
            .Columns
            .Where(x => x.Name == joinParameter.RigthTableJoinColumnName)
            .Select(x => PrepareColumnFullName(rightAlias, x.Name))
            .First();

        var resultTableSelectColumns =
            resultTable.Columns.Select(x => resultTable.GetColumnFullNameById(x.EtlColumnId));
        
        var joinQuery = ApplyJoin(rightTable, leftTable,joinParameter, leftAlias, rightAlias, firstJoinFullColumnName, secondJoinFullColumnName);
        joinQuery.Select(selectColumns);
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


    private string PrepareColumnFullName(string aliasName, string columnName)
    {
        if (aliasName == null) throw new ArgumentNullException(nameof(aliasName));
        if (columnName == null) throw new ArgumentNullException(nameof(columnName));
        return new StringBuilder(aliasName).Append('.').Append(columnName).ToString();
    }
}