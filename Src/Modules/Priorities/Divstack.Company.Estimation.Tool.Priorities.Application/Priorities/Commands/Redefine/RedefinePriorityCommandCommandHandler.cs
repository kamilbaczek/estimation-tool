﻿namespace Divstack.Company.Estimation.Tool.Priorities.Priorities.Commands.Redefine;

using Ardalis.GuardClauses;
using Common.Interfaces;
using Domain;
using Inquiries.Application.Common.Contracts;
using Inquiries.Application.Inquiries.Queries.GetClient;
using MediatR;

internal sealed class RedefinePriorityCommandCommandHandler : IRequestHandler<RedefinePriorityCommand>
{
    private readonly IInquiriesModule _inquiryModule;
    private readonly IIntegrationEventPublisher _integrationEventPublisher;
    private readonly IPrioritiesRepository _prioritiesRepository;
    public RedefinePriorityCommandCommandHandler(
        IPrioritiesRepository prioritiesRepository,
        IInquiriesModule inquiryModule,
        IIntegrationEventPublisher integrationEventPublisher)
    {
        _prioritiesRepository = prioritiesRepository;
        _inquiryModule = inquiryModule;
        _integrationEventPublisher = integrationEventPublisher;
    }
    public async Task<Unit> Handle(RedefinePriorityCommand command, CancellationToken cancellationToken)
    {
        var priorityId = new PriorityId(command.PriorityId);
        var priority = await _prioritiesRepository.GetAsync(priorityId, cancellationToken);
        if (priority is null)
            throw new NotFoundException(command.PriorityId.ToString(), nameof(Priority));

        var companySize = await GetCompanySize(command.InquiryId);
        priority.Redefine(companySize);

        await _prioritiesRepository.CommitAsync(priority, cancellationToken);
        if (priority.DomainEvents.Any())
            await _integrationEventPublisher.PublishAsync(priority.DomainEvents, cancellationToken);

        return Unit.Value;
    }

    private async Task<int?> GetCompanySize(Guid inquiryId)
    {
        var inquiryQuery = new GetInquiryClientQuery(inquiryId);
        var client = await _inquiryModule.ExecuteQueryAsync(inquiryQuery);

        return client.CompanySize;
    }
}
