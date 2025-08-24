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
        var _client = new HaloInfiniteClient(httpClient, "test-token", "xuid(12345)");
        IHaloInfiniteClient client = _client;

        var classType = _client.GetType();
        var interfaceType = client.GetType();

        var publicMethods = classType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        var interfaceMethods = interfaceType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        var missingMethods = publicMethods
            .Where(m => !interfaceMethods.Any(im =>
            {
                if (im.Name != m.Name)
                {
                    return false;
                }

                if (im.ReturnType != m.ReturnType)
                {
                    return false;
                }

                var imParams = im.GetParameters();
                var mParams = m.GetParameters();
                if (imParams.Length != mParams.Length)
                {
                    return false;
                }

                for (int i = 0; i < imParams.Length; i++)
                {
                    if (imParams[i].Name != mParams[i].Name || imParams[i].ParameterType != mParams[i].ParameterType)
                    {
                        return false;
                    }
                }

                return true;
            }))
            .ToList();

        if (missingMethods.Count > 0)
            throw new InvalidOperationException("Missing methods: " + string.Join(", ", missingMethods.Select(x => x.Name)));

        Console.WriteLine("Interface test passed! IHaloInfiniteClient correctly exposes all public members of HaloInfiniteClient.");
    }
}