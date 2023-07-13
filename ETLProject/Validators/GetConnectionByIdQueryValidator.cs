using ETLProject.Contract.DbConnectionContracts.Queries;
using FluentValidation;

namespace ETLProject.Validators;

public class GetConnectionByIdQueryValidator : AbstractValidator<GetConnectionByIdQuery>
{
    public GetConnectionByIdQueryValidator()
    {
        RuleFor(x => x.ConnectionId).NotEmpty();
    }
}