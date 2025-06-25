using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MergeIntervals.Core.Configuration;
using MergeIntervals.Core.Interfaces;
using MergeIntervals.Core.Services;

namespace MergeIntervals.Tests.Services;

/// <summary>
/// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
/// </summary>
public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddIntervalMerging_WithConfiguration_RegistersServicesCorrectly()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"IntervalMerger:EnablePerformanceLogging", "true"},
                {"IntervalMerger:MaxIntervals", "5000"}
            })
            .Build();

        // Act
        services.AddIntervalMerging(configuration);

        // Assert
        ServiceProvider provider = services.BuildServiceProvider();
        IIntervalMerger merger = provider.GetRequiredService<IIntervalMerger>();
        merger.Should().NotBeNull();

        IOptions<IntervalMergerOptions> options = provider.GetRequiredService<IOptions<IntervalMergerOptions>>();
        options.Value.EnablePerformanceLogging.Should().BeTrue();
        options.Value.MaxIntervals.Should().Be(5000);
    }

    [Fact]
    public void AddIntervalMerging_WithNullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection? services = null;
        IConfiguration configuration = new ConfigurationBuilder().Build();

        // Act & Assert
        Action act = () => services!.AddIntervalMerging(configuration);
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("services");
    }

    [Fact]
    public void AddIntervalMerging_WithNullConfiguration_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        IConfiguration? configuration = null;

        // Act & Assert
        Action act = () => services.AddIntervalMerging(configuration!);
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("configuration");
    }

    [Fact]
    public void AddIntervalMerging_WithCustomOptions_RegistersServicesCorrectly()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        // Act
        services.AddIntervalMerging(options =>
        {
            options.EnablePerformanceLogging = true;
            options.MaxIntervals = 2000;
        });

        // Assert
        ServiceProvider provider = services.BuildServiceProvider();
        IIntervalMerger merger = provider.GetRequiredService<IIntervalMerger>();
        merger.Should().NotBeNull();

        IOptions<IntervalMergerOptions> options = provider.GetRequiredService<IOptions<IntervalMergerOptions>>();
        options.Value.EnablePerformanceLogging.Should().BeTrue();
        options.Value.MaxIntervals.Should().Be(2000);
    }

    [Fact]
    public void AddIntervalMerging_WithCustomOptionsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        Action<IntervalMergerOptions>? configureOptions = null;

        // Act & Assert
        Action act = () => services.AddIntervalMerging(configureOptions!);
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("configureOptions");
    }

    [Fact]
    public void AddIntervalMerging_WithDefaultOptions_RegistersServicesCorrectly()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        // Act
        services.AddIntervalMerging();

        // Assert
        ServiceProvider provider = services.BuildServiceProvider();
        IIntervalMerger merger = provider.GetRequiredService<IIntervalMerger>();
        merger.Should().NotBeNull();

        IOptions<IntervalMergerOptions> options = provider.GetRequiredService<IOptions<IntervalMergerOptions>>();
        options.Value.EnablePerformanceLogging.Should().BeFalse(); // Default value
        options.Value.MaxIntervals.Should().Be(10000); // Default value
    }

    [Fact]
    public void AddIntervalMerging_WithDefaultOptionsNullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection? services = null;

        // Act & Assert
        Action act = () => services!.AddIntervalMerging();
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("services");
    }

    [Fact]
    public void AddIntervalMerging_RegistersAsSingleton()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        // Act
        services.AddIntervalMerging();

        // Assert
        ServiceProvider provider = services.BuildServiceProvider();
        IIntervalMerger merger1 = provider.GetRequiredService<IIntervalMerger>();
        IIntervalMerger merger2 = provider.GetRequiredService<IIntervalMerger>();
        
        merger1.Should().BeSameAs(merger2);
    }
} 