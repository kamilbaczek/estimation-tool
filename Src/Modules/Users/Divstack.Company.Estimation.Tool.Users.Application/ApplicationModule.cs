﻿using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Divstack.Company.Estimation.Tool.Users.Infrastructure")]

namespace Divstack.Company.Estimation.Tool.Users.Application;

using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EventBus.Publish.Extensions;

internal static class ApplicationModule
{
    internal static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddIntegrationEventsHandlers(typeof(ApplicationModule));

        return services;
    }
}
