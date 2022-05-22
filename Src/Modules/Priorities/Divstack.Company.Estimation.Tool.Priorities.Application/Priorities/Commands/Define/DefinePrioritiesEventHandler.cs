﻿namespace Divstack.Company.Estimation.Tool.Priorities.Priorities.Commands.Define;

using Domain;
using Domain.Deadlines;
using Inquiries.Application.Common.Contracts;
using Inquiries.Application.Inquiries.Queries.GetClient;
using MediatR;
using Shared.Infrastructure.EventBus.Subscribe;
using Valuations.IntegrationsEvents.ExternalEvents;

internal sealed class DefinePrioritiesEventHandler : IIntegrationEventHandler<ValuationRequested>
{
    private readonly IDeadlinesConfiguration _deadlinesConfiguration;
    private readonly IInquiriesModule _inquiryModule;
    private readonly IPrioritiesRepository _prioritiesRepository;

    public DefinePrioritiesEventHandler(IPrioritiesRepository prioritiesRepository,
        IDeadlinesConfiguration deadlinesConfiguration,
        IInquiriesModule inquiryModule)
    {
        _prioritiesRepository = prioritiesRepository;
        _deadlinesConfiguration = deadlinesConfiguration;
        _inquiryModule = inquiryModule;
    }

    public async ValueTask Handle(ValuationRequested proposalApprovedEvent, CancellationToken cancellationToken)
    {
        var deadline = Deadline.Create(_deadlinesConfiguration);
        var companySize = await GetCompanySize(proposalApprovedEvent.InquiryId);
        var valuationId = ValuationId.Create(proposalApprovedEvent.ValuationId);
        var inquiryId = InquiryId.Create(proposalApprovedEvent.InquiryId);

        var priority = Priority.Define(valuationId, inquiryId, companySize, deadline);

        await _prioritiesRepository.AddAsync(priority, cancellationToken);
    }

    private async Task<int?> GetCompanySize(Guid inquiryId)
    {
        var inquiryQuery = new GetInquiryClientQuery(inquiryId);
        var client = await _inquiryModule.ExecuteQueryAsync(inquiryQuery);

        return client.CompanySize;
    }
}
