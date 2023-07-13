using ETLProject.Contract.DbConnectionContracts.Commands;
using FluentValidation;

namespace ETLProject.Validators;

public class UpdateConnectionCommandValidator : AbstractValidator<UpdateConnectionCommand>
{
    public UpdateConnectionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}