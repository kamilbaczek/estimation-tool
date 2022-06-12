﻿namespace Divstack.Company.Estimation.Tool.Valuations.Infrastructure.Events.Publish.Mapper;

using Domain.Valuations.Events;
using Domain.Valuations.Proposals.Events;
using IntegrationsEvents.ExternalEvents;
using Messages;
using Shared.DDD.BuildingBlocks;

internal sealed class EventMapper : IEventMapper
{
    public IReadOnlyCollection<IntegrationEvent> Map(IReadOnlyCollection<IDomainEvent> events)
    {
        return events.Select(Map).ToList();
    }

    private static IntegrationEvent Map(IDomainEvent @event)
    {
        return @event switch
        {
            ProposalApprovedDomainEvent domainEvent =>
                new ProposalApproved(domainEvent.ValuationId.Value,
                    domainEvent.Proposal.Id.Value,
                    domainEvent.Proposal.SuggestedBy.Value,
                    domainEvent.Proposal.Price.Currency,
                    domainEvent.Proposal.Price.Value),
            ProposalCancelledDomainEvent domainEvent =>
                new ProposalCancelled(domainEvent.Proposal.CancelledBy.Value,
                    domainEvent.Proposal.Id.Value,
                    domainEvent.ValuationId.Value),
            ProposalRejectedDomainEvent domainEvent =>
                new ProposalRejected(
                    domainEvent.ValuationId.Value,
                    domainEvent.ProposalId.Value,
                    domainEvent.Value.Value,
                    domainEvent.Value.Currency),
            ProposalSuggestedDomainEvent domainEvent =>
                new ProposalSuggested(
                    domainEvent.ValuationId.Value,
                    domainEvent.Proposal.Id.Value,
                    domainEvent.InquiryId.Value,
                    domainEvent.Proposal.Price.Value,
                    domainEvent.Proposal.Price.Currency,
                    domainEvent.Proposal.Description.Message),
            ValuationRequestedDomainEvent domainEvent =>
                new ValuationRequested(
                    domainEvent.InquiryId.Value,
                    domainEvent.ValuationId.Value),
            ValuationCompletedDomainEvent domainEvent =>
                new ValuationCompleted(
                    domainEvent.InquiryId.Value,
                    domainEvent.ValuationId.Value,
                    domainEvent.Price.Value,
                    domainEvent.Price.Currency),
            _ => null
        };
    }
}
