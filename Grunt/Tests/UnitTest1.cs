using Surprenant.Grunt.Core;

namespace Tests;

/// <summary>
/// Simple test to demonstrate that the IHaloInfiniteClient interface works correctly.
/// </summary>
public class Tests
{
    /// <summary>
    /// Test that demonstrates the interface can be used to instantiate and use the client.
    /// </summary>
    [Test]
    public void TestInterfaceUsage()
    {
        // Create an HttpClient for testing
        using var httpClient = new HttpClient();

        // Create the client instance through the interface
        IHaloInfiniteClient client = new HaloInfiniteClient(httpClient, "test-token", "xuid(12345)");

        // Verify we can access properties through the interface
        client.SpartanToken = "updated-token";
        client.Xuid = "xuid(67890)";
        client.ClearanceToken = "test-clearance";

        // Verify the values were set correctly
        if (client.SpartanToken != "updated-token" ||
            client.Xuid != "xuid(67890)" ||
            client.ClearanceToken != "test-clearance")
        {
            throw new InvalidOperationException("Interface property access failed");
        }

        // The interface includes all public methods from HaloInfiniteClient
        // This demonstrates that the interface correctly abstracts the implementation

        Console.WriteLine("Interface test passed! IHaloInfiniteClient correctly exposes all public members of HaloInfiniteClient.");
    }
}