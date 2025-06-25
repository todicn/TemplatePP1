# HelloWorld Application

A simple C# application demonstrating the HelloWorld pattern with clean architecture, dependency injection, and comprehensive testing.

## 🚀 Features

- **Simple Greeting Service**: Basic "Hello, World!" functionality
- **Personalized Messages**: Customizable greetings with names
- **Message Service**: Welcome and farewell messages
- **Configuration Support**: Configurable through appsettings.json or code
- **Dependency Injection**: Fully integrated with Microsoft.Extensions.DependencyInjection
- **Comprehensive Testing**: Unit tests with xUnit
- **Clean Architecture**: Separation of concerns with interfaces and implementations

## 🏗️ Project Structure

```
HelloWorld/
├── HelloWorld.Core/                 # Core library
│   ├── Interfaces/                  # Service contracts
│   │   ├── IGreeter.cs             # Greeting service interface
│   │   └── IMessageService.cs     # Message service interface
│   ├── Implementations/            # Service implementations
│   │   ├── Greeter.cs              # Greeting service implementation
│   │   └── MessageService.cs      # Message service implementation
│   ├── Configuration/              # Configuration options
│   │   └── HelloWorldOptions.cs   # Application configuration
│   └── Services/                   # DI extensions
│       └── ServiceCollectionExtensions.cs
├── HelloWorld.Demo/                # Demo console application
│   ├── Program.cs                  # Entry point and demos
│   └── appsettings.json           # Configuration
├── HelloWorld.Tests/               # Unit tests
│   ├── Implementations/
│   │   ├── GreeterTests.cs
│   │   └── MessageServiceTests.cs
│   └── Services/
│       └── ServiceCollectionExtensionsTests.cs
└── build.ps1                      # Build script
```

## 🛠️ Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- PowerShell (for build script)

### Building the Project

```bash
# Using the build script (recommended)
./build.ps1

# Or manually
dotnet build --configuration Release
```

### Running Tests

```bash
dotnet test --configuration Release
```

### Running the Demo

```bash
dotnet run --project HelloWorld.Demo --configuration Release
```

## 💡 Usage

### Basic Usage with Dependency Injection

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HelloWorld.Core.Interfaces;
using HelloWorld.Core.Services;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddHelloWorld(options =>
        {
            options.DefaultName = "World";
            options.IncludeTimestamp = true;
        });
    })
    .Build();

var greeter = host.Services.GetRequiredService<IGreeter>();
var messageService = host.Services.GetRequiredService<IMessageService>();

Console.WriteLine(greeter.GetGreeting());              // "Hello, World!"
Console.WriteLine(greeter.GetGreeting("Alice"));       // "Hello, Alice!"
Console.WriteLine(messageService.GetWelcomeMessage()); // "Welcome to the HelloWorld application!"
```

### Configuration

Configure through `appsettings.json`:

```json
{
  "HelloWorld": {
    "DefaultName": "World",
    "IncludeTimestamp": true,
    "MessageFormat": "Hello, {0}!"
  }
}
```

Or through code:

```csharp
services.AddHelloWorld(options =>
{
    options.DefaultName = "Universe";
    options.IncludeTimestamp = false;
    options.MessageFormat = "Hi there, {0}!";
});
```

## 🧪 Testing

The project includes comprehensive unit tests covering:

- ✅ Greeter functionality with various input scenarios
- ✅ Message service operations
- ✅ Dependency injection configuration
- ✅ Service lifetime management

Run tests with:

```bash
dotnet test --configuration Release --logger:trx --collect:"XPlat Code Coverage"
```

## 🚀 Build & CI/CD

The project includes:

- **PowerShell build script** (`build.ps1`) for automated builds
- **GitHub Actions workflow** for continuous integration
- **Multi-platform support** (Windows, Linux, macOS)

## 📋 Features Demonstrated

This project demonstrates several important software engineering practices:

- **Clean Architecture**: Clear separation between interfaces and implementations
- **Dependency Injection**: Proper use of Microsoft.Extensions.DependencyInjection
- **Configuration Pattern**: Options pattern for configuration management
- **Unit Testing**: Comprehensive test coverage with xUnit
- **Documentation**: XML documentation comments for all public APIs
- **Build Automation**: Automated build and test pipeline

## 🤝 Contributing

This is a template project designed for pair programming and learning. Feel free to:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add/update tests
5. Submit a pull request

## 📄 License

This project is provided as-is for educational and demonstration purposes. 