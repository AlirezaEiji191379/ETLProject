using ETLProject.Common.Table;
using ETLProject.Contract.Limit;
using ETLProject.DataSource.QueryBusiness.LimitBusiness.Abstractions;
using FluentValidation;

namespace ETLProject.DataSource.QueryBusiness.LimitBusiness;

internal class LimitQueryBusiness : ILimitQueryBusiness
{
    private readonly IValidator<LimitContract> _validator;
    public LimitQueryBusiness(IValidator<LimitContract> validator)
    {
        _validator = validator;
    }
    public ETLTable AddLimitQuery(ETLTable etlTable, LimitContract limitContract)
    {
        _validator.ValidateAndThrow(limitContract);
        var query = etlTable.Query;
        query.Limit(limitContract.Top);
        return etlTable;
    }
}