using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Surprenant.Grunt.Core;
using Surprenant.Grunt.Core.Storage;
using Surprenant.Grunt.Models;
using System.Net;
using System.Net.Http;

namespace Surprenant.Grunt.Util;

/// <summary>
/// Provides extension method to register the Halo Infinite client and related services in the ASP.NET Core dependency injection container.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Registers <see cref="IHaloInfiniteClient"/>, <see cref="IHaloInfiniteClientFactory"/>, and other related services in the DI container.
    /// If you wish to, register your own implementation of <see cref="IOAuthStorage"/>, or <see cref="IAccountAuthorization"/> as singletons BEFORE calling this method.
    /// This will allow you to decide on your own storage mechanism for OAuth tokens and account authorizations (Database, api, file, console input).
    /// The default behavior is to store in files in the current working directory (tokens.json and AccountAuthorization.txt).
    /// </summary>
    /// <param name="builder"></param>
    public static void RegisterHaloInfiniteClientFactory(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<IHaloInfiniteClient, HaloInfiniteClient>((services, config) =>
        {
        })
        .ConfigurePrimaryHttpMessageHandler(s =>
        {
            return new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
            };
        });

        builder.Services.TryAddSingleton<IOAuthStorage, OAuthStorage>();
        builder.Services.TryAddSingleton<IAccountAuthorization, AccountAuthorization>();
        builder.Services.AddSingleton<IStateSeed, StateSeed>();
        builder.Services.AddSingleton<IHaloInfiniteClientFactory, HaloInfiniteClientFactory>();
        builder.Services.Configure<ClientConfiguration>(builder.Configuration.GetSection(nameof(ClientConfiguration)));
    }
}
