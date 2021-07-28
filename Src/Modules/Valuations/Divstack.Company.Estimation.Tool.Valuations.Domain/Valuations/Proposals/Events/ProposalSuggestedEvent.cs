﻿using Divstack.Company.Estimation.Tool.Shared.DDD.BuildingBlocks;
using Divstack.Company.Estimation.Tool.Shared.DDD.ValueObjects;
using Divstack.Company.Estimation.Tool.Shared.DDD.ValueObjects.Emails;

namespace Divstack.Company.Estimation.Tool.Valuations.Domain.Valuations.Proposals.Events
{
    public sealed class ProposalSuggestedEvent : DomainEventBase
    {
        internal ProposalSuggestedEvent(
            string fullName,
            EmployeeId proposedBy,
            ProposalId proposalId,
            Money value,
            Email clientEmail,
            ProposalDescription description,
            ValuationId valuationId)
        {
            ProposedBy = proposedBy;
            Value = value;
            ProposalId = proposalId;
            ClientEmail = clientEmail;
            FullName = fullName;
            Description = description;
            ValuationId = valuationId;
        }

        public ProposalId ProposalId { get; }
        public Email ClientEmail { get; }
        public EmployeeId ProposedBy { get; }
        public Money Value { get; }
        public string FullName { get; }
        public ProposalDescription Description { get; }
        public ValuationId ValuationId { get; }
    }
}