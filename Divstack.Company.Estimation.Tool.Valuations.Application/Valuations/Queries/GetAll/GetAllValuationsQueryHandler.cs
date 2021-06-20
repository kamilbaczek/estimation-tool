﻿using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Divstack.Company.Estimation.Tool.Valuations.Application.Interfaces;
using MediatR;

namespace Divstack.Company.Estimation.Tool.Valuations.Application.Valuations.Queries.GetAll
{
    internal sealed class GetAllValuationsQueryHandler : IRequestHandler<GetAllValuationsQuery, ValuationListVm>
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

        public GetAllValuationsQueryHandler(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }

        public async Task<ValuationListVm> Handle(GetAllValuationsQuery request, CancellationToken cancellationToken)
        {
            var connection = _databaseConnectionFactory.Create();

            var valuationItems = await connection.QueryAsync<ValuationListItemDto>(
                new CommandDefinition(@$"
                SELECT [Id] AS {nameof(ValuationListItemDto.Id)},
                       [Enquiry_Client_FirstName] AS {nameof(ValuationListItemDto.FirstName)},
                       [Enquiry_Client_LastName]  AS {nameof(ValuationListItemDto.LastName)},
                       [RecentHistoryEntry].[Status_Value] AS {nameof(ValuationListItemDto.Status)},
                       [RequestedDate] AS {nameof(ValuationListItemDto.RequestedDate)},
                       [CompletedBy] AS {nameof(ValuationListItemDto.CompletedBy)}
                FROM [Valuations].[Valuations] OUTER apply
                  (SELECT top 1 Status_Value
                   FROM Valuations.History
                   WHERE Valuations.History.ValuationId = [Valuations].[Valuations].[Id]
                   ORDER BY ChangeDate DESC) [RecentHistoryEntry]
                 ORDER BY [RequestedDate] DESC
        ", cancellationToken));

            return new ValuationListVm(valuationItems);
        }
    }
}