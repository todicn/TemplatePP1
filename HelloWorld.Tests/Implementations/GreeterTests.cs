using Xunit;
using HelloWorld.Core.Implementations;

namespace HelloWorld.Tests.Implementations;

/// <summary>
/// Unit tests for the Greeter class.
/// </summary>
public class GreeterTests
{
    [Fact]
    public void GetGreeting_WithNoName_ReturnsDefaultGreeting()
    {
        // Arrange
        var greeter = new Greeter();

        // Act
        string result = greeter.GetGreeting();

        // Assert
        Assert.Equal("Hello, World!", result);
    }

    [Fact]
    public void GetGreeting_WithEmptyName_ReturnsDefaultGreeting()
    {
        // Arrange
        var greeter = new Greeter();

        // Act
        string result = greeter.GetGreeting("");

        // Assert
        Assert.Equal("Hello, World!", result);
    }

    [Fact]
    public void GetGreeting_WithWhitespaceName_ReturnsDefaultGreeting()
    {
        // Arrange
        var greeter = new Greeter();

        // Act
        string result = greeter.GetGreeting("   ");

        // Assert
        Assert.Equal("Hello, World!", result);
    }

    [Fact]
    public void GetGreeting_WithValidName_ReturnsPersonalizedGreeting()
    {
        // Arrange
        var greeter = new Greeter();

        // Act
        string result = greeter.GetGreeting("Alice");

        // Assert
        Assert.Equal("Hello, Alice!", result);
    }

    [Theory]
    [InlineData("John", "Hello, John!")]
    [InlineData("Jane", "Hello, Jane!")]
    [InlineData("Bob", "Hello, Bob!")]
    public void GetGreeting_WithVariousNames_ReturnsCorrectGreeting(string name, string expected)
    {
        // Arrange
        var greeter = new Greeter();

        // Act
        string result = greeter.GetGreeting(name);

        // Assert
        Assert.Equal(expected, result);
    }
} 