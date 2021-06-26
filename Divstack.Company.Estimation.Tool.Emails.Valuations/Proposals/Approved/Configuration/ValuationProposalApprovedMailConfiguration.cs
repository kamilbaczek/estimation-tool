﻿using Divstack.Company.Estimation.Tool.Shared.Abstractions.Configuration;
using Microsoft.Extensions.Configuration;

namespace Divstack.Company.Estimation.Tool.Emails.Valuations.Proposals.Approved.Configuration
{
    internal class ValuationProposalApprovedMailConfiguration : ConfigurationBase,
        IValuationProposalApprovedMailConfiguration
    {
        public ValuationProposalApprovedMailConfiguration(IConfiguration configuration) : base(configuration,
            nameof(ValuationProposalApprovedMailConfiguration))
        {
        }

        public string Subject => configurationSection.GetValue<string>(nameof(Subject));
        public string PathToTemplate => configurationSection.GetValue<string>(nameof(PathToTemplate));
        public string ApprovedProposalLink => configurationSection.GetValue<string>(nameof(PathToTemplate));
    }
}