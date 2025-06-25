using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;
using HelloWorld.Core.Configuration;
using HelloWorld.Core.Interfaces;
using HelloWorld.Core.Services;

namespace HelloWorld.Tests.Services;

/// <summary>
/// Unit tests for ServiceCollectionExtensions.
/// </summary>
public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddHelloWorld_WithDefaultOptions_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddHelloWorld();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var greeter = serviceProvider.GetService<IGreeter>();
        var messageService = serviceProvider.GetService<IMessageService>();
        
        Assert.NotNull(greeter);
        Assert.NotNull(messageService);
    }

    [Fact]
    public void AddHelloWorld_WithOptionsAction_ConfiguresOptions()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddHelloWorld(options =>
        {
            options.DefaultName = "Test";
            options.IncludeTimestamp = true;
        });
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var greeter = serviceProvider.GetService<IGreeter>();
        Assert.NotNull(greeter);
    }

    [Fact]
    public void AddHelloWorld_WithConfiguration_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["DefaultName"] = "ConfigTest",
                ["IncludeTimestamp"] = "true"
            })
            .Build();

        // Act
        services.AddHelloWorld(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var greeter = serviceProvider.GetService<IGreeter>();
        var messageService = serviceProvider.GetService<IMessageService>();
        
        Assert.NotNull(greeter);
        Assert.NotNull(messageService);
    }

    [Fact]
    public void AddHelloWorld_RegistersCorrectImplementations()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddHelloWorld();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var greeter = serviceProvider.GetService<IGreeter>();
        var messageService = serviceProvider.GetService<IMessageService>();
        
        Assert.IsType<HelloWorld.Core.Implementations.Greeter>(greeter);
        Assert.IsType<HelloWorld.Core.Implementations.MessageService>(messageService);
    }

    [Fact]
    public void AddHelloWorld_ServicesAreScoped()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddHelloWorld();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        using (var scope1 = serviceProvider.CreateScope())
        using (var scope2 = serviceProvider.CreateScope())
        {
            var greeter1 = scope1.ServiceProvider.GetService<IGreeter>();
            var greeter2 = scope1.ServiceProvider.GetService<IGreeter>();
            var greeter3 = scope2.ServiceProvider.GetService<IGreeter>();

            // Assert
            Assert.Same(greeter1, greeter2); // Same within scope
            Assert.NotSame(greeter1, greeter3); // Different across scopes
        }
    }
} 