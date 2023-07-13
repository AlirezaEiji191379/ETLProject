using ETLProject.Contract.DbConnectionContracts.Queries;
using FluentValidation;

namespace ETLProject.Validators;

public class GetDataBaseTablesQueryValidator : AbstractValidator<GetDatabaseTablesQuery>
{
    public GetDataBaseTablesQueryValidator()
    {
        RuleFor(x => x.ConnectionId).NotEmpty();
        RuleFor(x => x.DatabaseName).NotEmpty();
    }
}