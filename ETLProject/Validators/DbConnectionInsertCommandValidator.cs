using ETLProject.Contract.DbConnectionContracts.Commands;
using FluentValidation;

namespace ETLProject.Validators;

public class DbConnectionInsertCommandValidator : AbstractValidator<DbConnectionInsertCommand>
{
    public DbConnectionInsertCommandValidator()
    {
        RuleFor(x => x.ConnectionDto).SetValidator(new ConnectionDtoValidator());
    }
}

