﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Divstack.Company.Estimation.Tool.Inquiries.Application.Extensions;
using Divstack.Company.Estimation.Tool.Inquiries.Application.Inquiries.Commands.Make.Dtos;
using Divstack.Company.Estimation.Tool.Inquiries.Application.Interfaces;
using Divstack.Company.Estimation.Tool.Inquiries.Domain.Clients;
using Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries;
using Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries.Clients;
using Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries.Item.Services;
using Divstack.Company.Estimation.Tool.Services.Core.Services.Contracts;
using Divstack.Company.Estimation.Tool.Shared.DDD.ValueObjects.Emails;
using MediatR;

namespace Divstack.Company.Estimation.Tool.Inquiries.Application.Inquiries.Commands.Make
{
    internal sealed class MakeInquiryCommandHandler : IRequestHandler<MakeInquiryCommand>
    {
        private readonly IClientCompanyFinder _clientCompanyFinder;
        private readonly IInquiriesRepository _inquiriesRepository;
        private readonly IIntegrationEventPublisher _integrationEventPublisher;
        private readonly IServiceExistingChecker _serviceExistingChecker;
        private readonly IMapper<ServiceDto, Service> _serviceMapper;

        public MakeInquiryCommandHandler(IInquiriesRepository inquiriesRepository,
            IServiceExistingChecker serviceExistingChecker,
            IIntegrationEventPublisher integrationEventPublisher,
            IClientCompanyFinder clientCompanyFinder,
            IMapper<ServiceDto, Service> serviceMapper)
        {
            _inquiriesRepository = inquiriesRepository;
            _serviceExistingChecker = serviceExistingChecker;
            _integrationEventPublisher = integrationEventPublisher;
            _clientCompanyFinder = clientCompanyFinder;
            _serviceMapper = serviceMapper;
        }

        public async Task<Unit> Handle(MakeInquiryCommand makeInquiryCommand, CancellationToken cancellationToken)
        {
            var email = Email.Of(makeInquiryCommand.Email);
            var clientCompany = await _clientCompanyFinder.FindCompany(email);
            var client = Client.Of(email, makeInquiryCommand.FirstName, makeInquiryCommand.LastName, clientCompany);
            var services = makeInquiryCommand.Services
                .Select(service => _serviceMapper.Map(service))
                .ToReadonly();

            var inquiry = await Inquiry.MakeAsync(services, client, _serviceExistingChecker);

            await _inquiriesRepository.PersistAsync(inquiry, cancellationToken);
            _integrationEventPublisher.Publish(inquiry.DomainEvents);

            return Unit.Value;
        }
    }
}