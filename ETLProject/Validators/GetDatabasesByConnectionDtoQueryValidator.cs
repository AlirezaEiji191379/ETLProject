using ETLProject.Contract.DbConnectionContracts.Queries;
using FluentValidation;

namespace ETLProject.Validators;

public class GetDatabasesByConnectionDtoQueryValidator : AbstractValidator<GetDatabasesByConnectionDtoQuery>
{
    public GetDatabasesByConnectionDtoQueryValidator()
    {
        RuleFor(x => x.ConnectionDto).SetValidator(new ConnectionDtoValidator());
    }
}