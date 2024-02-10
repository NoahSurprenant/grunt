using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenSpartan.Grunt.Core;

namespace Grunt.ApiTest;

public class InfiniteClientHealthCheck : IHealthCheck
{
    private readonly HaloInfiniteClientFactory _clientFactory;
    public InfiniteClientHealthCheck(HaloInfiniteClientFactory haloInfiniteClientFactory)
    {
        _clientFactory = haloInfiniteClientFactory;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var client = await _clientFactory.CreateAsync();

        var response = await client.StatsGetMatchStats("21416434-4717-4966-9902-af7097469f74");

        if (response is null || response.Result is null)
            return HealthCheckResult.Unhealthy("Response was null");

        return HealthCheckResult.Healthy("Success");
    }
}
