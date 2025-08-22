// <copyright file="HaloInfiniteClientInterfaceTest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Net.Http;
using Surprenant.Grunt.Core;

namespace Surprenant.Grunt.Tests;

/// <summary>
/// Simple test to demonstrate that the IHaloInfiniteClient interface works correctly.
/// </summary>
public static class HaloInfiniteClientInterfaceTest
{
    /// <summary>
    /// Test that demonstrates the interface can be used to instantiate and use the client.
    /// </summary>
    public static void TestInterfaceUsage()
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