﻿namespace Divstack.Company.Estimation.Tool.Valuations.Application.IntegrationTests.Common.Assertions;

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using netDumbster.smtp;

internal static class MailAssertions
{
    internal static void AssertEqualsToAddress(this SmtpMessage left, string right)
    {
        left.ToAddresses.Length.Should().Be(1);
        left.ToAddresses.First().Address.Should().Be(right);
    }

    internal static void AssertEqualsFromAddress(this SmtpMessage email, IConfiguration configuration)
    {
        var emailFromConfiguration = MailConfiguration.MailFrom(configuration);
        email.FromAddress.Address.Should().Be(emailFromConfiguration);
    }
}
