using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenSpartan.Grunt.Core;
using OpenSpartan.Grunt.Models;
using System.Net;
using System.Net.Http;

namespace OpenSpartan.Grunt.Util;
public static class ServiceExtensions
{
    public static void RegisterHaloInfiniteClientFactory(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<HaloInfiniteClient>((services, config) =>
        {
        })
        .ConfigurePrimaryHttpMessageHandler(s =>
        {
            return new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
            };
        });

        builder.Services.AddSingleton<IAccountAuthorization, AccountAuthorization>();
        builder.Services.AddSingleton<IStateSeed, StateSeed>();
        builder.Services.AddSingleton<HaloInfiniteClientFactory>();
        builder.Services.Configure<ClientConfiguration>(builder.Configuration.GetSection(nameof(ClientConfiguration)));
    }
}
