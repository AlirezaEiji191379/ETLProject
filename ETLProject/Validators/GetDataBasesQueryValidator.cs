using ETLProject.Contract.DbConnectionContracts.Queries;
using FluentValidation;

namespace ETLProject.Validators;

public class GetDataBasesQueryValidator : AbstractValidator<GetDatabasesQuery>
{
    public GetDataBasesQueryValidator()
    {
        RuleFor(x => x.ConnectionId).NotEmpty();
    }
}