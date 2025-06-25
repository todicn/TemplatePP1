using HelloWorld.Core.Implementations;

namespace HelloWorld.Demo;

/// <summary>
/// Simple HelloWorld demo application.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== HelloWorld Demo Application ===");
        Console.WriteLine();

        // Create instances directly - no DI needed
        var greeter = new Greeter();
        var messageService = new MessageService();

        // Demo 1: Basic greeting
        await DemoBasicGreeting(greeter);

        // Demo 2: Personalized greetings
        await DemoPersonalizedGreetings(greeter);

        // Demo 3: Message service
        await DemoMessageService(messageService);

        Console.WriteLine();
        Console.WriteLine("All demonstrations completed successfully!");
        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    /// <summary>
    /// Demonstrates basic greeting functionality.
    /// </summary>
    private static async Task DemoBasicGreeting(Greeter greeter)
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
    private static async Task DemoPersonalizedGreetings(Greeter greeter)
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
    private static async Task DemoMessageService(MessageService messageService)
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
