﻿using ETLProject.Contract.Sort;
using FluentValidation;

namespace ETLProject.DataSource.QueryBusiness.SortBusiness.Validator;

public class OrderColumnDtoValidator : AbstractValidator<OrderColumnDto>
{
    public OrderColumnDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}