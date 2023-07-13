using ETLProject.Contract.DbConnectionContracts.Commands;
using FluentValidation;

namespace ETLProject.Validators;

public class DeleteConnectionCommandValidator : AbstractValidator<DeleteConnectionCommand>
{
    public DeleteConnectionCommandValidator()
    {
        RuleFor(x => x.ConnectionId).NotEmpty();
    }
}