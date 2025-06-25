# MergeIntervals

A robust C# library for merging overlapping intervals with support for dependency injection, configuration options, and comprehensive testing.

## 🚀 Features

- **Thread-Safe Operations**: All implementations are thread-safe for concurrent usage
- **Dependency Injection**: Full support for Microsoft.Extensions.DependencyInjection
- **Configuration Options**: Configurable via IOptions pattern
- **Async/Await Support**: Both synchronous and asynchronous APIs
- **Performance Optimized**: Supports parallel processing for large datasets
- **Comprehensive Testing**: High test coverage with xUnit and FluentAssertions
- **XML Documentation**: Complete XML documentation for all public APIs

## 📋 Requirements

Given a collection of intervals, merge all overlapping intervals.

**Example:**
```
Input: [[1, 4], [2, 6], [8, 10], [15, 18]]
Output: [[1, 6], [8, 10], [15, 18]]
```

## 🛠 Installation

### Prerequisites
- .NET 8.0 or later

### Clone the Repository
```bash
git clone https://github.com/todicn/MergeIntervalsPP1.git
cd MergeIntervalsPP1
```

### Build the Solution
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Run Demo
```bash
dotnet run --project MergeIntervals.Demo
```

## 📖 Usage

### Basic Usage

```csharp
using MergeIntervals.Core.Implementations;
using MergeIntervals.Core.Interfaces;

// Create intervals
List<IInterval> intervals = new()
{
    new Interval(1, 4),
    new Interval(2, 6),
    new Interval(8, 10),
    new Interval(15, 18)
};

// Create merger with default options
var options = Microsoft.Extensions.Options.Options.Create(new IntervalMergerOptions());
var merger = new IntervalMerger(options);

// Merge intervals
IEnumerable<IInterval> result = merger.Merge(intervals);
// Result: [[1, 6], [8, 10], [15, 18]]
```

### Dependency Injection

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MergeIntervals.Core.Services;

// Setup DI container
IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddIntervalMerging(options =>
        {
            options.EnablePerformanceLogging = true;
            options.MaxIntervals = 10000;
            options.UseParallelProcessing = true;
        });
    })
    .Build();

// Get service and use
var merger = host.Services.GetRequiredService<IIntervalMerger>();
var result = await merger.MergeAsync(intervals);
```

### Configuration Options

```csharp
public class IntervalMergerOptions
{
    public bool EnablePerformanceLogging { get; set; } = false;
    public int MaxIntervals { get; set; } = 10000;
    public bool UseParallelProcessing { get; set; } = true;
    public int ParallelProcessingThreshold { get; set; } = 1000;
}
```

## 🏗 Architecture

The project follows clean architecture principles with clear separation of concerns:

```
MergeIntervals.Core/
├── Interfaces/          # Contracts and abstractions
├── Implementations/     # Core business logic
├── Services/           # DI extensions and services
└── Configuration/      # Configuration options

MergeIntervals.Tests/
├── Implementations/    # Tests for core implementations
└── Services/          # Tests for service layer

MergeIntervals.Demo/    # Console application demonstrating usage
```

## 🧪 Testing

The project includes comprehensive unit tests covering:

- ✅ Happy path scenarios
- ✅ Edge cases (empty lists, single intervals, negative numbers)
- ✅ Error conditions and validation
- ✅ Thread-safety testing
- ✅ Performance scenarios
- ✅ Dependency injection setup

### Test Coverage
- **Interval Class**: Constructor validation, equality, hash codes
- **IntervalMerger Class**: All merge scenarios, async operations, configuration
- **ServiceCollectionExtensions**: DI registration and configuration

## 📊 Performance

The library is optimized for performance:

- **Sorting**: O(n log n) time complexity for sorting intervals
- **Merging**: O(n) time complexity for merging sorted intervals
- **Parallel Processing**: Automatic parallel processing for large datasets (>1000 intervals)
- **Memory Efficient**: Minimal memory allocations in hot paths

## 🔧 Configuration

### appsettings.json
```json
{
  "IntervalMerger": {
    "EnablePerformanceLogging": true,
    "MaxIntervals": 10000,
    "UseParallelProcessing": true,
    "ParallelProcessingThreshold": 1000
  }
}
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Add tests for your changes
5. Ensure all tests pass (`dotnet test`)
6. Commit your changes (`git commit -m 'Add amazing feature'`)
7. Push to the branch (`git push origin feature/amazing-feature`)
8. Open a Pull Request

## 📝 Code Style

This project follows the coding standards defined in `.cursorrules`:

- C# naming conventions (PascalCase for classes/methods, camelCase for fields)
- Explicit type declarations for readability
- Async/await patterns for public APIs
- Nullable reference types with proper null checking
- XML documentation for all public APIs
- Thread-safe implementations

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🎯 Examples

### Example 1: Basic Merging
```csharp
Input:  [[1, 4], [2, 6], [8, 10], [15, 18]]
Output: [[1, 6], [8, 10], [15, 18]]
```

### Example 2: No Overlapping
```csharp
Input:  [[1, 2], [3, 5], [6, 7]]
Output: [[1, 2], [3, 5], [6, 7]]
```

### Example 3: All Overlapping
```csharp
Input:  [[1, 4], [2, 5], [3, 6]]
Output: [[1, 6]]
```

### Example 4: Touching Intervals
```csharp
Input:  [[1, 4], [4, 5]]
Output: [[1, 5]]
```

## 🚨 Error Handling

The library provides comprehensive error handling:

- `ArgumentNullException`: When null intervals are provided
- `ArgumentException`: When intervals exceed maximum allowed count
- `ArgumentException`: When interval start > end

## 📞 Support

For support, please open an issue on GitHub or contact the maintainers.

---

**Happy Coding! 🎉** 