using HelloWorld.Core.Interfaces;

namespace HelloWorld.Core.Implementations;

/// <summary>
/// A simple implementation of the greeter interface.
/// </summary>
public class Greeter : IGreeter
{
    /// <summary>
    /// Gets a greeting message.
    /// </summary>
    /// <param name="name">The name to include in the greeting. If null or empty, uses a default greeting.</param>
    /// <returns>A greeting message.</returns>
    public string GetGreeting(string? name = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Hello, World!";
        }

        return $"Hello, {name}!";
    }
} 