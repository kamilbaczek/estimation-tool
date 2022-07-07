﻿namespace Divstack.Company.Estimation.Tool.Valuations.Application.IntegrationTests;

using Bootstrapper;
using Common.Engine;
using Common.Engine.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

[Binding]
public class TestEngine
{
    internal static IServiceScopeFactory? ServiceScopeFactory;

    [BeforeFeature]
    public static async Task RunBeforeAnyTests()
    {
        await PersistenceContainer.StartAsync();

        var configuration = BuildConfiguration();
        var startup = new Startup(configuration);
        var services = new ServiceCollection();
        startup.ConfigureServices(services);

        services.ReplaceHostEnvironment();
        services.ReplaceIntegrationEventPublisher();
        services.ReplaceCurrentUserServiceToMock();

        ServiceScopeFactory = services.BuildServiceProvider()
            .GetService<IServiceScopeFactory>();
    }

    [AfterFeature]
    public static void RunAfterAnyTests()
    {
        PersistenceContainer.Stop();
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .AddUserSecrets<TestEngine>();

        return builder.Build();
    }
}
