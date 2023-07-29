using ETLProject.Common.Table;
using ETLProject.Contract.Join;
using ETLProject.DataSource.Common.Exceptions;
using ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.JoinBusiness.Validation;

internal class JoinValidator : IJoinValidator
{
    public void ValidateJoinParameter(ETLTable leftTable, ETLTable rightTable, JoinParameter joinParameter)
    {
        foreach (var columnName in joinParameter.LeftTableSelectedColumnNames.Where(columnName =>
                     leftTable.Columns.FirstOrDefault(x => x.Name == columnName) == null))
        {
            throw new ColumnDoesNotExistException($"the column with name {columnName} does not exist in left table");
        }

        foreach (var columnName in joinParameter.RigthTableSelectedColumnNames.Where(columnName =>
                     rightTable.Columns.FirstOrDefault(x => x.Name == columnName) == null))
        {
            throw new ColumnDoesNotExistException($"the column with name {columnName} does not exist in right table");
        }

        if (!leftTable.Columns.Select(x => x.Name).Contains(joinParameter.LeftTableJoinColumnName))
        {
            throw new ColumnDoesNotExistException(
                $"the column with name {joinParameter.LeftTableJoinColumnName} does not exist in left table");
        }
        if (!rightTable.Columns.Select(x => x.Name).Contains(joinParameter.RigthTableJoinColumnName))
        {
            throw new ColumnDoesNotExistException(
                $"the column with name {joinParameter.RigthTableJoinColumnName} does not exist in right table");
        }
    }
}