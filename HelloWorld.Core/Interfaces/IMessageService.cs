namespace HelloWorld.Core.Interfaces;

/// <summary>
/// Defines the contract for message services.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Gets a welcome message.
    /// </summary>
    /// <returns>A welcome message string.</returns>
    string GetWelcomeMessage();

    /// <summary>
    /// Gets a farewell message.
    /// </summary>
    /// <returns>A farewell message string.</returns>
    string GetFarewellMessage();
} 