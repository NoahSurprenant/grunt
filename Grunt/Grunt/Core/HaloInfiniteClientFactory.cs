using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Surprenant.Grunt.Authentication;
using Surprenant.Grunt.Core.Storage;
using Surprenant.Grunt.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Surprenant.Grunt.Core;

/// <summary>
/// A factory intended to be registered as a singleton.
/// </summary>
public class HaloInfiniteClientFactory : IHaloInfiniteClientFactory
{
    private readonly IOptionsMonitor<ClientConfiguration> _options;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<HaloInfiniteClientFactory> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAccountAuthorization _accountAuthorization;
    private readonly IOAuthStorage _oAuthStorage;

    public HaloInfiniteClientFactory(IOptionsMonitor<ClientConfiguration> optionsMonitor, ILoggerFactory loggerFactory, IConfiguration config,
        IHttpClientFactory httpClientFactory, IAccountAuthorization accountAuthorization, IOAuthStorage oAuthStorage)
    {
        _options = optionsMonitor;
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<HaloInfiniteClientFactory>();
        _configuration = config;
        _httpClientFactory = httpClientFactory;
        _accountAuthorization = accountAuthorization;
        _oAuthStorage = oAuthStorage;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="IHaloInfiniteClient"/> every time this method is called and ensures it has valid Spartan Token, if it cannot do so then it throws.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception">Exception thrown when not configured correctly or otherwise unable to create valid spartan token</exception>
    public async Task<IHaloInfiniteClient> CreateAsync()
    {
        var clientConfig = _options.CurrentValue;
        if (clientConfig is null) throw new Exception("ClientConfiguration is null!");
        if (string.IsNullOrEmpty(clientConfig.ClientId)) throw new Exception("ClientId is null or empty");
        if (string.IsNullOrEmpty(clientConfig.ClientSecret)) throw new Exception("ClientSecret is null or empty");
        if (string.IsNullOrEmpty(clientConfig.RedirectUrl)) throw new Exception("RedirectUrl is null or empty");

        XboxAuthenticationClient manager = new();

        HaloAuthenticationClient haloAuthClient = new();

        OAuthToken? currentOAuthToken = null;

        var ticket = new XboxTicket();
        var haloTicket = new XboxTicket();
        var extendedTicket = new XboxTicket();
        var haloToken = new SpartanToken();

        var token = await _oAuthStorage.GetToken();
        if (token is null)
        {
            _logger.LogDebug("Requesting new OAuth token");
            currentOAuthToken = await RequestNewToken(manager, clientConfig);
        }

        ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
        if (ticket == null)
        {
            // There was a failure to obtain the user token, so likely we need to refresh.
            currentOAuthToken = await manager.RefreshOAuthToken(
                clientConfig.ClientId,
                currentOAuthToken.RefreshToken,
                clientConfig.RedirectUrl,
                clientConfig.ClientSecret);

            if (currentOAuthToken == null)
            {
                _logger.LogInformation("Could not get the token even with the refresh token.");
                currentOAuthToken = await RequestNewToken(manager, clientConfig);
            }
            ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
        }

        haloTicket = await manager.RequestXstsToken(ticket.Token);

        extendedTicket = await manager.RequestXstsToken(ticket.Token, false);

        haloToken = await haloAuthClient.GetSpartanToken(haloTicket.Token);

        _logger.LogDebug("Your Halo token: {HaloToken}", haloToken.Token);

        return new HaloInfiniteClient(_httpClientFactory.CreateClient(nameof(HaloInfiniteClient)), haloToken.Token, extendedTicket.DisplayClaims.Xui[0].Xid);
    }

    private async Task<OAuthToken> RequestNewToken(XboxAuthenticationClient manager, ClientConfiguration clientConfig)
    {
        var code = await _accountAuthorization.GetCodeAsync();

        // If no local token file exists, request a new set of tokens.
        var currentOAuthToken = await manager.RequestOAuthToken(clientConfig.ClientId, code, clientConfig.RedirectUrl, clientConfig.ClientSecret);

        if (currentOAuthToken is null)
        {
            _logger.LogWarning("No token was obtained. There is no valid token to be used right now.");
            return null;
        }

        await _oAuthStorage.SetToken(currentOAuthToken);

        return currentOAuthToken;
    }
}
