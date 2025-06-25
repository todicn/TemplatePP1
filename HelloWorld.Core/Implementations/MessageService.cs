using HelloWorld.Core.Interfaces;

namespace HelloWorld.Core.Implementations;

/// <summary>
/// A simple implementation of the message service interface.
/// </summary>
public class MessageService : IMessageService
{
    /// <summary>
    /// Gets a welcome message.
    /// </summary>
    /// <returns>A welcome message string.</returns>
    public string GetWelcomeMessage()
    {
        return "Welcome to the HelloWorld application!";
    }

    /// <summary>
    /// Gets a farewell message.
    /// </summary>
    /// <returns>A farewell message string.</returns>
    public string GetFarewellMessage()
    {
        return "Thank you for using HelloWorld. Goodbye!";
    }
} 