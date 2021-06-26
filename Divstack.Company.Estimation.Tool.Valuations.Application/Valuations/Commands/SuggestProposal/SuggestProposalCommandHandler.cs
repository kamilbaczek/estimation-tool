﻿using System.Threading;
using System.Threading.Tasks;
using Divstack.Company.Estimation.Tool.Shared.DDD.ValueObjects;
using Divstack.Company.Estimation.Tool.Valuations.Application.Interfaces;
using Divstack.Company.Estimation.Tool.Valuations.Domain.UserAccess;
using Divstack.Company.Estimation.Tool.Valuations.Domain.Valuations;
using MediatR;

namespace Divstack.Company.Estimation.Tool.Valuations.Application.Valuations.Commands.SuggestProposal
{
    internal sealed class SuggestProposalCommandHandler : IRequestHandler<SuggestProposalCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IValuationsRepository _valuationsRepository;

        public SuggestProposalCommandHandler(IValuationsRepository valuationsRepository,
            ICurrentUserService currentUserService,
            IEventPublisher eventPublisher)
        {
            _valuationsRepository = valuationsRepository;
            _currentUserService = currentUserService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(SuggestProposalCommand command, CancellationToken cancellationToken)
        {
            var valuationId = new ValuationId(command.ValuationId);
            var valuation = await _valuationsRepository.GetAsync(valuationId, cancellationToken);
            var employeeId = new EmployeeId(_currentUserService.GetPublicUserId());
            var money = Money.Of(command.Value, command.Currency);

            valuation.SuggestProposal(money, command.Description, employeeId);

            await _valuationsRepository.CommitAsync(cancellationToken);
            _eventPublisher.Publish(valuation.DomainEvents);
            return Unit.Value;
        }
    }
}