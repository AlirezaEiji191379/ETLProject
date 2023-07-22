using ETLProject.Contract.DbConnectionContracts;
using FluentValidation;

namespace ETLProject.Validators;

public class ConnectionDtoValidator : AbstractValidator<ConnectionDto>
{
    public ConnectionDtoValidator()
    {
        RuleFor(x => x.Host).NotEmpty();
        RuleFor(x => x.Port).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}