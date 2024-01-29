using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenSpartan.Grunt.Endpoints;
using OpenSpartan.Grunt.Models;
using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSpartan.Grunt.Util
{
    public interface IAccountAuthorization
    {
        /// <summary>
        /// Returns one time use code and deletes. If no code exist then throws exception
        /// </summary>
        /// <returns>One time use authorization code</returns>
        Task<string> GetCodeAsync();

        /// <summary>
        /// Sets authorization code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task SetCodeAsync(string code);
    }

    internal class AccountAuthorization : IAccountAuthorization
    {
        public AccountAuthorization(ILogger<AccountAuthorization> logger, IStateSeed stateSeed, IOptionsMonitor<ClientConfiguration> optionsMonitor)
        {
            _logger = logger;
            _stateSeed = stateSeed;
            _options = optionsMonitor;
        }

        private readonly ILogger<AccountAuthorization> _logger;
        private readonly IStateSeed _stateSeed;
        private readonly IOptionsMonitor<ClientConfiguration> _options;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Grabs code from text file and deletes text file. If file does not exist throws exception
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCodeAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (File.Exists("AccountAuthorization.txt"))
                {
                    var code = await File.ReadAllTextAsync("AccountAuthorization.txt");
                    File.Delete("AccountAuthorization.txt");
                    return code;
                }
                else
                {
                    var clientConfig = _options.CurrentValue;

                    var url = GenerateAuthUrl(clientConfig.ClientId, clientConfig.RedirectUrl, state: _stateSeed.State.ToString());
                    _logger.LogError("Admin must provide AccountAuthorization: {URL}", url);
                    throw new Exception("Missing AccountAuthorization. Contact admin");
                }
            }
            finally { _semaphore.Release(); }
        }

        public async Task SetCodeAsync(string code)
        {
            await _semaphore.WaitAsync();
            try
            {
                await System.IO.File.WriteAllTextAsync("AccountAuthorization.txt", code);
            }
            finally { _semaphore.Release(); }
        }

        /// <summary>
        /// Generates the authentication URL that can be used to produce the temporary code
        /// for subsequent Xbox Live authentication flows.
        /// </summary>
        /// <param name="clientId">Client ID defined for the registered application in the Azure Portal.</param>
        /// <param name="redirectUrl">Redirect URL defined for the registered application in the Azure Portal.</param>
        /// <param name="scopes">A list of scopes used for authentication against the Xbox Live APIs.</param>
        /// <param name="state">Temporary state indicator.</param>
        /// <returns>Returns the full authentication URL that can be pasted in a web browser.</returns>
        private string GenerateAuthUrl(string clientId, string redirectUrl, string[]? scopes = null, string state = "")
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("client_id", clientId);
            queryString.Add("response_type", "code");
            queryString.Add("approval_prompt", "auto");

            if (scopes != null && scopes.Length > 0)
            {
                queryString.Add("scope", string.Join(" ", scopes));
            }
            else
            {
                queryString.Add("scope", string.Join(" ", GlobalConstants.DEFAULT_AUTH_SCOPES));
            }

            queryString.Add("redirect_uri", redirectUrl);

            if (!string.IsNullOrEmpty(state))
            {
                queryString.Add("state", state);
            }

            return XboxEndpoints.XboxLiveAuthorize + "?" + queryString.ToString();
        }
    }
}
