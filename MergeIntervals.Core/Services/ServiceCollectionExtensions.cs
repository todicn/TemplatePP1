using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MergeIntervals.Core.Configuration;
using MergeIntervals.Core.Implementations;
using MergeIntervals.Core.Interfaces;

namespace MergeIntervals.Core.Services;

/// <summary>
/// Extension methods for configuring services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds interval merging services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration instance to bind options from.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services or configuration is null.</exception>
    public static IServiceCollection AddIntervalMerging(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        // Configure options
        services.Configure<IntervalMergerOptions>(configuration.GetSection(IntervalMergerOptions.SectionName));

        // Register services
        services.AddSingleton<IIntervalMerger, IntervalMerger>();

        return services;
    }

    /// <summary>
    /// Adds interval merging services to the dependency injection container with custom options.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configureOptions">An action to configure the interval merger options.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services or configureOptions is null.</exception>
    public static IServiceCollection AddIntervalMerging(this IServiceCollection services, Action<IntervalMergerOptions> configureOptions)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        // Configure options
        services.Configure(configureOptions);

        // Register services
        services.AddSingleton<IIntervalMerger, IntervalMerger>();

        return services;
    }

    /// <summary>
    /// Adds interval merging services to the dependency injection container with default options.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services is null.</exception>
    public static IServiceCollection AddIntervalMerging(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        return services.AddIntervalMerging(_ => { });
    }
} 