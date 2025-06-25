using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MergeIntervals.Core.Implementations;
using MergeIntervals.Core.Interfaces;
using MergeIntervals.Core.Services;

namespace MergeIntervals.Demo;

/// <summary>
/// Demo application showcasing the interval merging functionality.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== MergeIntervals Demo Application ===");
        Console.WriteLine();

        // Setup dependency injection
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddIntervalMerging(options =>
                {
                    options.EnablePerformanceLogging = true;
                    options.MaxIntervals = 10000;
                });
                services.AddLogging(builder => builder.AddConsole());
            })
            .Build();

        IServiceProvider serviceProvider = host.Services;
        ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        IIntervalMerger intervalMerger = serviceProvider.GetRequiredService<IIntervalMerger>();

        try
        {
            logger.LogInformation("Starting interval merging demonstrations");

            // Demo 1: Basic example from requirements
            await DemoRequirementExample(intervalMerger);

            // Demo 2: Various test cases
            await DemoVariousTestCases(intervalMerger);

            // Demo 3: Edge cases
            await DemoEdgeCases(intervalMerger);

            // Demo 4: Performance test
            await DemoPerformanceTest(intervalMerger);

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
    /// Demonstrates the basic example from the requirements.
    /// </summary>
    private static async Task DemoRequirementExample(IIntervalMerger intervalMerger)
    {
        Console.WriteLine("=== Demo 1: Basic Requirement Example ===");
        Console.WriteLine("Input: [[1, 4], [2, 6], [8, 10], [15, 18]]");
        Console.WriteLine("Expected: [[1, 6], [8, 10], [15, 18]]");

        List<IInterval> intervals = new()
        {
            new Interval(1, 4),
            new Interval(2, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        IEnumerable<IInterval> result = await intervalMerger.MergeAsync(intervals);
        
        Console.WriteLine($"Result: {FormatIntervals(result)}");
        Console.WriteLine();
    }

    /// <summary>
    /// Demonstrates various test cases.
    /// </summary>
    private static async Task DemoVariousTestCases(IIntervalMerger intervalMerger)
    {
        Console.WriteLine("=== Demo 2: Various Test Cases ===");

        // Test Case 1: No overlapping intervals
        Console.WriteLine("Test Case 1 - No overlapping intervals:");
        Console.WriteLine("Input: [[1, 2], [3, 5], [6, 7]]");
        List<IInterval> intervals1 = new()
        {
            new Interval(1, 2),
            new Interval(3, 5),
            new Interval(6, 7)
        };
        IEnumerable<IInterval> result1 = intervalMerger.Merge(intervals1);
        Console.WriteLine($"Result: {FormatIntervals(result1)}");
        Console.WriteLine();

        // Test Case 2: All intervals overlap
        Console.WriteLine("Test Case 2 - All intervals overlap:");
        Console.WriteLine("Input: [[1, 4], [2, 5], [3, 6]]");
        List<IInterval> intervals2 = new()
        {
            new Interval(1, 4),
            new Interval(2, 5),
            new Interval(3, 6)
        };
        IEnumerable<IInterval> result2 = await intervalMerger.MergeAsync(intervals2);
        Console.WriteLine($"Result: {FormatIntervals(result2)}");
        Console.WriteLine();

        // Test Case 3: Touching intervals
        Console.WriteLine("Test Case 3 - Touching intervals:");
        Console.WriteLine("Input: [[1, 4], [4, 5], [6, 8]]");
        List<IInterval> intervals3 = new()
        {
            new Interval(1, 4),
            new Interval(4, 5),
            new Interval(6, 8)
        };
        IEnumerable<IInterval> result3 = intervalMerger.Merge(intervals3);
        Console.WriteLine($"Result: {FormatIntervals(result3)}");
        Console.WriteLine();
    }

    /// <summary>
    /// Demonstrates edge cases.
    /// </summary>
    private static async Task DemoEdgeCases(IIntervalMerger intervalMerger)
    {
        Console.WriteLine("=== Demo 3: Edge Cases ===");

        // Edge Case 1: Empty list
        Console.WriteLine("Edge Case 1 - Empty list:");
        List<IInterval> emptyIntervals = new();
        IEnumerable<IInterval> emptyResult = await intervalMerger.MergeAsync(emptyIntervals);
        Console.WriteLine($"Result: {FormatIntervals(emptyResult)}");
        Console.WriteLine();

        // Edge Case 2: Single interval
        Console.WriteLine("Edge Case 2 - Single interval:");
        Console.WriteLine("Input: [[5, 10]]");
        List<IInterval> singleInterval = new() { new Interval(5, 10) };
        IEnumerable<IInterval> singleResult = intervalMerger.Merge(singleInterval);
        Console.WriteLine($"Result: {FormatIntervals(singleResult)}");
        Console.WriteLine();

        // Edge Case 3: Negative numbers
        Console.WriteLine("Edge Case 3 - Negative numbers:");
        Console.WriteLine("Input: [[-5, -1], [-3, 2], [0, 5]]");
        List<IInterval> negativeIntervals = new()
        {
            new Interval(-5, -1),
            new Interval(-3, 2),
            new Interval(0, 5)
        };
        IEnumerable<IInterval> negativeResult = await intervalMerger.MergeAsync(negativeIntervals);
        Console.WriteLine($"Result: {FormatIntervals(negativeResult)}");
        Console.WriteLine();
    }

    /// <summary>
    /// Demonstrates performance with a larger dataset.
    /// </summary>
    private static async Task DemoPerformanceTest(IIntervalMerger intervalMerger)
    {
        Console.WriteLine("=== Demo 4: Performance Test ===");
        Console.WriteLine("Generating 1000 random intervals...");

        Random random = new(42); // Fixed seed for reproducible results
        List<IInterval> largeIntervals = new();

        for (int i = 0; i < 1000; i++)
        {
            int start = random.Next(0, 10000);
            int end = start + random.Next(1, 100);
            largeIntervals.Add(new Interval(start, end));
        }

        Console.WriteLine($"Generated {largeIntervals.Count} intervals");
        
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
        IEnumerable<IInterval> result = await intervalMerger.MergeAsync(largeIntervals);
        stopwatch.Stop();

        Console.WriteLine($"Merged into {result.Count()} intervals");
        Console.WriteLine($"Processing time: {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine();
    }

    /// <summary>
    /// Formats a collection of intervals for display.
    /// </summary>
    private static string FormatIntervals(IEnumerable<IInterval> intervals)
    {
        if (!intervals.Any())
        {
            return "[]";
        }

        return "[" + string.Join(", ", intervals.Select(i => i.ToString())) + "]";
    }
}
