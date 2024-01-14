using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenSpartan.Grunt.Core;
using OpenSpartan.Grunt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpartan.Grunt.Util;
public static class ServiceExtensions
{
    public static void RegisterHaloInfiniteClientFactory(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("InfiteClient", (services, config) =>
        {

        });

        builder.Services.AddSingleton<HaloInfiniteClientFactory>();
        builder.Services.Configure<ClientConfiguration>(builder.Configuration.GetSection(nameof(ClientConfiguration)));
    }
}
