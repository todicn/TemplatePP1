namespace HelloWorld.Core.Interfaces;

/// <summary>
/// Represents a simple greeter contract.
/// </summary>
public interface IGreeter
{
    /// <summary>
    /// Gets a greeting message.
    /// </summary>
    /// <param name="name">The name to include in the greeting. If null or empty, uses a default greeting.</param>
    /// <returns>A greeting message.</returns>
    string GetGreeting(string? name = null);
} 