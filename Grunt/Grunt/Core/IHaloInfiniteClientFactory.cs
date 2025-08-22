using System;
using System.Threading.Tasks;

namespace Surprenant.Grunt.Core;

/// <summary>
/// A factory intended to be registered as a singleton.
/// </summary>
public interface IHaloInfiniteClientFactory
{
    /// <summary>
    /// Creates a new instance of the <see cref="IHaloInfiniteClient"/> every time this method is called and ensures it has valid Spartan Token, if it cannot do so then it throws.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception">Exception thrown when not configured correctly or otherwise unable to create valid spartan token</exception>
    Task<IHaloInfiniteClient> CreateAsync();
}
