﻿namespace Divstack.Company.Estimation.Tool.Payments.Infrastructure.Mediation.Decorators;

using Logging;
using Microsoft.Extensions.DependencyInjection;

internal static class DecoratorsModule
{
    internal static IServiceCollection AddDecorators(this IServiceCollection services)
    {
        services.AddLoggingDecorator();

        return services;
    }
}