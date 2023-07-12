using ETLProject.Contract.Sort;
using FluentValidation;

namespace ETLProject.DataSource.QueryBusiness.SortBusiness.Validator;

internal class SortContractValidator : AbstractValidator<SortContract>
{
    public SortContractValidator()
    {
        RuleForEach(x => x.Columns).SetValidator(new OrderColumnDtoValidator());
    }
}