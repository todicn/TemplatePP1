namespace HelloWorld.Core.Configuration;

/// <summary>
/// Configuration options for the HelloWorld application.
/// </summary>
public class HelloWorldOptions
{
    /// <summary>
    /// Gets or sets the default greeting name when none is provided.
    /// </summary>
    public string DefaultName { get; set; } = "World";

    /// <summary>
    /// Gets or sets whether to include timestamps in messages.
    /// </summary>
    public bool IncludeTimestamp { get; set; } = false;

    /// <summary>
    /// Gets or sets the message format template.
    /// </summary>
    public string MessageFormat { get; set; } = "Hello, {0}!";
} 