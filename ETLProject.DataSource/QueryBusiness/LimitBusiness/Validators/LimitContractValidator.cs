using ETLProject.Contract.Limit;
using FluentValidation;
using Humanizer.DateTimeHumanizeStrategy;

namespace ETLProject.DataSource.QueryBusiness.LimitBusiness.Validators;

internal class LimitContractValidator : AbstractValidator<LimitContract>
{
    public LimitContractValidator()
    {
        RuleFor(x => x.Top)
            .GreaterThanOrEqualTo(1)
            .WithMessage("the limit operator must be greater than 1.");
    }
}