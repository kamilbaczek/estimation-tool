﻿namespace Divstack.Company.Estimation.Tool.Priorities.Priorities.Commands.Archive;

using Ardalis.GuardClauses;
using Common.Interfaces;
using Domain;
using MediatR;
using Valuations.IntegrationsEvents.ExternalEvents;

internal sealed class ArchivePriorityCommandCommandHandler : INotificationHandler<ProposalSuggested>
{
    private readonly IPrioritiesRepository _prioritiesRepository;
    private readonly IIntegrationEventPublisher _integrationEventPublisher;
    public ArchivePriorityCommandCommandHandler(IPrioritiesRepository prioritiesRepository,
        IIntegrationEventPublisher integrationEventPublisher)
    {
        _prioritiesRepository = prioritiesRepository;
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(ProposalSuggested @event, CancellationToken cancellationToken)
    {
        var valuationId = ValuationId.Create(@event.ValuationId);
        var priority = await _prioritiesRepository.GetAsync(valuationId, cancellationToken);
        if (priority is null)
            throw new NotFoundException(@event.ValuationId.ToString(), nameof(Priority));

        priority.Archive();

        await _prioritiesRepository.CommitAsync(priority, cancellationToken);
        await _integrationEventPublisher.PublishAsync(priority.DomainEvents, cancellationToken);
    }
}