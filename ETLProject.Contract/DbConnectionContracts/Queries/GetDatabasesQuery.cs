﻿using MediatR;

namespace ETLProject.Contract.DbConnectionContracts.Queries;

public class GetDatabasesQuery : IRequest<ResponseDto>
{
    public Guid ConnectionId { get; set; }
}