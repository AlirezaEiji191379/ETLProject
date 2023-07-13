using ETLProject.Contract.DbConnectionContracts.Queries;
using FluentValidation;

namespace ETLProject.Validators;

public class GetTableColumnInfoQueryValidator : AbstractValidator<GetTableColumnInfosQuery>
{
    public GetTableColumnInfoQueryValidator()
    {
        RuleFor(x => x.ConnectionId).NotEmpty();
        RuleFor(x => x.DatabaseName).NotEmpty();
        RuleFor(x => x.TableName).NotEmpty();
    }
}