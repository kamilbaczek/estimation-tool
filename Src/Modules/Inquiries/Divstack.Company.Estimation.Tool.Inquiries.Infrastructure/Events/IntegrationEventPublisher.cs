﻿using System.Collections.Generic;
using Divstack.Company.Estimation.Tool.Inquiries.Application.Interfaces;
using Divstack.Company.Estimation.Tool.Inquiries.Infrastructure.Events.Mapper;
using Divstack.Company.Estimation.Tool.Shared.DDD.BuildingBlocks;
using MediatR;

namespace Divstack.Company.Estimation.Tool.Inquiries.Infrastructure.Events
{
    internal sealed class IntegrationEventPublisher : IIntegrationEventPublisher
    {
        private readonly IEventMapper _eventMapper;
        private readonly IMediator _mediator;

        public IntegrationEventPublisher(IMediator mediator, IEventMapper eventMapper)
        {
            _mediator = mediator;
            _eventMapper = eventMapper;
        }

        public void Publish(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            var integrationEvents = _eventMapper.Map(domainEvents);
            foreach (var integrationEvent in integrationEvents) _mediator.Publish(integrationEvent);
        }
    }
}