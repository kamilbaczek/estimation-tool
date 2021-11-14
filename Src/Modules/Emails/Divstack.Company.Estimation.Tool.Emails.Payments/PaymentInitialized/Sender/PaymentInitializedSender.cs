﻿namespace Divstack.Company.Estimation.Tool.Emails.Payments.PaymentInitialized.Sender;

using Common.Extensions;
using Configuration;
using Microsoft.Extensions.Configuration;
using Modules.Emails.Core.Sender.Contracts;
using Modules.Emails.Core.Sender.TemplateReader;

internal sealed class PaymentInitializedSender : IPaymentInitializedSender
{
    private const string InquiryIdPlaceholder = "{InquiryId}";
    private const string PaymentIdPlaceholder = "{PaymentId}";
    private const string AmountToPayPlaceholder = "{AmountToPay}";
    private const string ClientFullNamePlaceholder = "{ClientFullName}";
    private const string PaymentLinkPlaceholder = "{PaymentLink}";
    private readonly IPaymentInitializedMailConfiguration _paymentInitializedMailConfiguration;
    private readonly IEmailSender _emailSender;
    private readonly IMailTemplateReader _mailTemplateReader;

    public PaymentInitializedSender(IMailTemplateReader mailTemplateReader,
        IConfiguration configuration,
        IEmailSender emailSender)
    {
        _mailTemplateReader = mailTemplateReader;
        _paymentInitializedMailConfiguration = new PaymentInitializedMailConfiguration(configuration);
        _emailSender = emailSender;
    }

    public void Send(PaymentInitializedEmailRequest request)
    {
        var htmlTemplate = _mailTemplateReader.Read(_paymentInitializedMailConfiguration.PathToTemplate);
        var paymentLink = _paymentInitializedMailConfiguration.PaymentUrl.ReplaceIgnoreCases(PaymentIdPlaceholder, request.PaymentId);
        var emailAsHtml = htmlTemplate
            .ReplaceIgnoreCases(InquiryIdPlaceholder, request.PaymentId)
            .ReplaceIgnoreCases(AmountToPayPlaceholder, request.AmountToPay)
            .ReplaceIgnoreCases(ClientFullNamePlaceholder, request.ClientFullName)
            .ReplaceIgnoreCases(PaymentLinkPlaceholder, paymentLink);

        _emailSender.Send(request.ClientEmail, _paymentInitializedMailConfiguration.Subject, emailAsHtml);
    }
}
