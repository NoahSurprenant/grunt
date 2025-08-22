# Interface Generation for HaloInfiniteClient

This document explains how the `IHaloInfiniteClient` interface is automatically generated from the `HaloInfiniteClient` class.

## Overview

The `IHaloInfiniteClient` interface is automatically generated to include all public methods and properties from the `HaloInfiniteClient` class. This ensures that:

1. The interface always stays in sync with the class implementation
2. Any changes to public methods are automatically reflected in the interface
3. The interface provides a clean abstraction for dependency injection and testing

## How it Works

### GenerateInterface Attribute

The `HaloInfiniteClient` class is marked with the `[GenerateInterface]` attribute:

```csharp
[GenerateInterface]
public class HaloInfiniteClient : IHaloInfiniteClient
{
    // Class implementation...
}
```

This attribute indicates that an interface should be generated for this class.

### Automatic Generation

The interface is generated using the `Tools/InterfaceGenerator.cs` utility, which:

1. Parses the `HaloInfiniteClient.cs` file
2. Extracts all public properties and methods
3. Generates the corresponding interface declarations
4. Creates the `IHaloInfiniteClient.cs` file

### Generated Interface Structure

The generated interface includes:

- **Properties**: `SpartanToken`, `Xuid`, `ClearanceToken`
- **Methods**: All public async methods that return `Task<HaloApiResultContainer<T, HaloApiErrorContainer>>`

Example generated interface members:

```csharp
public interface IHaloInfiniteClient
{
    string SpartanToken { get; set; }
    string Xuid { get; set; }
    string ClearanceToken { get; set; }
    
    Task<HaloApiResultContainer<ApiSettingsContainer, HaloApiErrorContainer>> GetApiSettingsContainer();
    Task<HaloApiResultContainer<BotCustomizationData, HaloApiErrorContainer>> AcademyGetBotCustomization(string flightId);
    // ... all other public methods
}
```

## Regenerating the Interface

When you add, modify, or remove public methods from `HaloInfiniteClient`, you need to regenerate the interface:

### Using the Script

Run the regeneration script from the repository root:

```bash
./regenerate-interface.sh
```

### Manual Regeneration

If you prefer to regenerate manually:

1. Navigate to a temporary directory
2. Create a new console application: `dotnet new console`
3. Copy `Tools/InterfaceGenerator.cs` to `Program.cs`
4. Run: `dotnet run`

## Benefits

1. **Consistency**: The interface is always consistent with the class
2. **Maintainability**: No need to manually update interface when methods change
3. **Type Safety**: Compile-time verification that the class implements all interface members
4. **Testing**: Easy to create mock implementations for unit testing
5. **Dependency Injection**: Clean interface for IoC container registration

## Usage Example

```csharp
// Register in dependency injection container
services.AddScoped<IHaloInfiniteClient, HaloInfiniteClient>();

// Use through interface
public class MyService
{
    private readonly IHaloInfiniteClient _client;
    
    public MyService(IHaloInfiniteClient client)
    {
        _client = client;
    }
    
    public async Task<ApiSettingsContainer> GetSettings()
    {
        var result = await _client.GetApiSettingsContainer();
        return result.Result;
    }
}
```

## Files

- `Grunt/Grunt/Core/IHaloInfiniteClient.cs` - The generated interface (auto-generated, do not edit manually)
- `Grunt/Grunt/Core/HaloInfiniteClient.cs` - The implementation class (marked with `[GenerateInterface]`)
- `Grunt/Grunt/Attributes/GenerateInterfaceAttribute.cs` - The attribute used to mark classes for interface generation
- `Tools/InterfaceGenerator.cs` - The utility that generates the interface
- `regenerate-interface.sh` - Script to regenerate the interface
- `Grunt/Grunt/Tests/HaloInfiniteClientInterfaceTest.cs` - Test demonstrating interface usage

## Note

The generated interface file (`IHaloInfiniteClient.cs`) should not be edited manually as it will be overwritten when the interface is regenerated. All changes should be made to the `HaloInfiniteClient` class instead.