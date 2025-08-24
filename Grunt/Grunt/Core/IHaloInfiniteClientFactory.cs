using System;
using System.Threading.Tasks;

namespace Surprenant.Grunt.Core;

/// <summary>
/// A factory intended to be registered as a singleton. Use CreateAsync to create a new instance of <see cref="IHaloInfiniteClient"/> with a valid Spartan Token.
/// Or use any of the methods from <see cref="IHaloInfiniteClient"/> directly and <see cref="IHaloInfiniteClientFactory"/> will proxy them to a new or cached instance of <see cref="IHaloInfiniteClient"/>."/>
/// </summary>
public interface IHaloInfiniteClientFactory : IHaloInfiniteClient
{
    /// <summary>
    /// Creates a new instance of the <see cref="IHaloInfiniteClient"/> every time this method is called and ensures it has valid Spartan Token, if it cannot do so then it throws.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception">Exception thrown when not configured correctly or otherwise unable to create valid spartan token</exception>
    Task<IHaloInfiniteClient> CreateAsync();
}
