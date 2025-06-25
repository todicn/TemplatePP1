using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HelloWorld.Core.Configuration;
using HelloWorld.Core.Interfaces;
using HelloWorld.Core.Implementations;

namespace HelloWorld.Core.Services;

/// <summary>
/// Extension methods for configuring HelloWorld services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds HelloWorld services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Optional action to configure HelloWorld options.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddHelloWorld(
        this IServiceCollection services,
        Action<HelloWorldOptions>? configureOptions = null)
    {
        // Configure options
        if (configureOptions != null)
        {
            services.Configure(configureOptions);
        }
        else
        {
            services.Configure<HelloWorldOptions>(options => { });
        }

        // Register services
        services.AddScoped<IGreeter, Greeter>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }

    /// <summary>
    /// Adds HelloWorld services to the service collection with configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration section.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddHelloWorld(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<HelloWorldOptions>(configuration);
        services.AddScoped<IGreeter, Greeter>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
} 