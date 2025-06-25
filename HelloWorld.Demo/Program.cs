using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HelloWorld.Core.Interfaces;
using HelloWorld.Core.Services;

namespace HelloWorld.Demo;

/// <summary>
/// Demo application showcasing the HelloWorld functionality.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== HelloWorld Demo Application ===");
        Console.WriteLine();

        // Setup dependency injection
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHelloWorld(options =>
                {
                    options.DefaultName = "World";
                    options.IncludeTimestamp = true;
                    options.MessageFormat = "Hello, {0}!";
                });
                services.AddLogging(builder => builder.AddConsole());
            })
            .Build();

        IServiceProvider serviceProvider = host.Services;
        ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        IGreeter greeter = serviceProvider.GetRequiredService<IGreeter>();
        IMessageService messageService = serviceProvider.GetRequiredService<IMessageService>();

        try
        {
            logger.LogInformation("Starting HelloWorld demonstrations");

            // Demo 1: Basic greeting
            await DemoBasicGreeting(greeter);

            // Demo 2: Personalized greetings
            await DemoPersonalizedGreetings(greeter);

            // Demo 3: Message service
            await DemoMessageService(messageService);

            logger.LogInformation("All demonstrations completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during demonstration");
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    /// <summary>
    /// Demonstrates basic greeting functionality.
    /// </summary>
    private static async Task DemoBasicGreeting(IGreeter greeter)
    {
        Console.WriteLine("=== Demo 1: Basic Greeting ===");
        
        string greeting = greeter.GetGreeting();
        Console.WriteLine(greeting);
        Console.WriteLine();
        
        await Task.Delay(500); // Small delay for demo effect
    }

    /// <summary>
    /// Demonstrates personalized greetings.
    /// </summary>
    private static async Task DemoPersonalizedGreetings(IGreeter greeter)
    {
        Console.WriteLine("=== Demo 2: Personalized Greetings ===");
        
        string[] names = { "Alice", "Bob", "Charlie", "Diana" };
        
        foreach (string name in names)
        {
            string greeting = greeter.GetGreeting(name);
            Console.WriteLine(greeting);
            await Task.Delay(300); // Small delay for demo effect
        }
        
        Console.WriteLine();
    }

    /// <summary>
    /// Demonstrates message service functionality.
    /// </summary>
    private static async Task DemoMessageService(IMessageService messageService)
    {
        Console.WriteLine("=== Demo 3: Message Service ===");
        
        string welcomeMessage = messageService.GetWelcomeMessage();
        Console.WriteLine(welcomeMessage);
        
        await Task.Delay(1000); // Pause between messages
        
        string farewellMessage = messageService.GetFarewellMessage();
        Console.WriteLine(farewellMessage);
        
        Console.WriteLine();
    }
}
