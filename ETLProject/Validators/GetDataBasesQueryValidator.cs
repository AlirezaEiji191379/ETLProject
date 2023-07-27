using ETLProject.Contract.DbConnectionContracts.Queries;
using FluentValidation;

namespace ETLProject.Validators;

public class GetDataBasesQueryValidator : AbstractValidator<GetDatabasesByConnectionIdQuery>
{
    public GetDataBasesQueryValidator()
    {
        RuleFor(x => x.ConnectionId).NotEmpty();
    }
}