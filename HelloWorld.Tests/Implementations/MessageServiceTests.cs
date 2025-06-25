using Xunit;
using HelloWorld.Core.Implementations;

namespace HelloWorld.Tests.Implementations;

/// <summary>
/// Unit tests for the MessageService class.
/// </summary>
public class MessageServiceTests
{
    [Fact]
    public void GetWelcomeMessage_ReturnsExpectedMessage()
    {
        // Arrange
        var messageService = new MessageService();

        // Act
        string result = messageService.GetWelcomeMessage();

        // Assert
        Assert.Equal("Welcome to the HelloWorld application!", result);
    }

    [Fact]
    public void GetFarewellMessage_ReturnsExpectedMessage()
    {
        // Arrange
        var messageService = new MessageService();

        // Act
        string result = messageService.GetFarewellMessage();

        // Assert
        Assert.Equal("Thank you for using HelloWorld. Goodbye!", result);
    }

    [Fact]
    public void GetWelcomeMessage_IsNotNullOrEmpty()
    {
        // Arrange
        var messageService = new MessageService();

        // Act
        string result = messageService.GetWelcomeMessage();

        // Assert
        Assert.False(string.IsNullOrEmpty(result));
    }

    [Fact]
    public void GetFarewellMessage_IsNotNullOrEmpty()
    {
        // Arrange
        var messageService = new MessageService();

        // Act
        string result = messageService.GetFarewellMessage();

        // Assert
        Assert.False(string.IsNullOrEmpty(result));
    }
} 