using ETLProject.Contract.DbConnectionContracts.Commands;
using FluentValidation;

namespace ETLProject.Validators;

public class TestConnectionCommandValidator : AbstractValidator<TestConnectionCommand>
{
    public TestConnectionCommandValidator()
    {
        RuleFor(x => x.ConnectionDto).SetValidator(new ConnectionDtoValidator());
    }
}