using Microsoft.Extensions.Logging;
using Surprenant.Grunt.Models;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Surprenant.Grunt.Core.Storage;
public class OAuthStorage : IOAuthStorage
{
    private const string DefaultPath = "tokens.json";
    private readonly string Path;
    private readonly ILogger _logger;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    /// <summary>
    /// Creates a new instance of the OAuthStorage class, if path is null then defaults to tokens.json.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="path">Optional path to store tokens to</param>
    public OAuthStorage(ILogger<OAuthStorage> logger, string? path = null)
    {
        Path = path ?? DefaultPath;
        _logger = logger;
    }

    public async Task SetToken(OAuthToken token)
    {
        await _semaphore.WaitAsync();
        try
        {
            string json = JsonSerializer.Serialize(token);
            await System.IO.File.WriteAllTextAsync(Path, json);
            _logger.LogWarning("Stored the tokens locally.");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<OAuthToken?> GetToken()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (System.IO.File.Exists(Path))
            {
                _logger.LogDebug("Reading {Path}", Path);

                using var stream = new StreamReader(Path);
                string json = await stream.ReadToEndAsync();

                var token = JsonSerializer.Deserialize<OAuthToken>(json);

                return token;
            }
            return null;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}

public interface IOAuthStorage
{
    Task SetToken(OAuthToken token);
    Task<OAuthToken?> GetToken();
}