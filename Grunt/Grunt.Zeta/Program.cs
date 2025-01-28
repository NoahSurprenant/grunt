using Surprenant.Grunt.Models;
using Surprenant.Grunt.Authentication;
using Surprenant.Grunt.Core;
using Surprenant.Grunt.Models.HaloInfinite;
using Surprenant.Grunt.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace OpenSpartan.Grunt.Zeta;

class Program
{
    static async Task Main(string[] args)
    {
        var clientConfig = ConfigurationReader.ReadConfiguration<ClientConfiguration>("client.json");
        
        XboxAuthenticationClient manager = new();
        var url = manager.GenerateAuthUrl(clientConfig.ClientId, clientConfig.RedirectUrl);

        HaloAuthenticationClient haloAuthClient = new();

        OAuthToken currentOAuthToken = null;

        var ticket = new XboxTicket();
        var haloTicket = new XboxTicket();
        var extendedTicket = new XboxTicket();
        var haloToken = new SpartanToken();

        if (System.IO.File.Exists("tokens.json"))
        {
            Console.WriteLine("Trying to use local tokens...");

            // If a local token file exists, load the file.
            currentOAuthToken = ConfigurationReader.ReadConfiguration<OAuthToken>("tokens.json");
        }
        else
        {
            currentOAuthToken = RequestNewToken(url, manager, clientConfig);
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
                Console.WriteLine("Could not get the token even with the refresh token.");
                currentOAuthToken = RequestNewToken(url, manager, clientConfig);
            }
            ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
        }

        haloTicket = await manager.RequestXstsToken(ticket.Token);

        extendedTicket = await manager.RequestXstsToken(ticket.Token, false);

        haloToken = await haloAuthClient.GetSpartanToken(haloTicket.Token);
        Console.WriteLine("Your Halo token:");
        Console.WriteLine(haloToken.Token);
        
        var handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
        };
        HaloInfiniteClient client = new(new(handler), haloToken.Token, extendedTicket.DisplayClaims.Xui[0].Xid);

        // Test getting the clearance for local execution.
        string localClearance = string.Empty;
        var clearance = (await client.SettingsGetClearance("RETAIL", "UNUSED", "222249.22.06.08.1730-0")).Result;
        if (clearance != null)
        {
            localClearance = clearance.FlightConfigurationId;
            client.ClearanceToken = localClearance;
            Console.WriteLine($"Your clearance is {localClearance} and it's set in the client.");
        }
        else
        {
            Console.WriteLine("Could not obtain the clearance.");
        }

        // Try getting actual Halo Infinite data.
        var example = await client.StatsGetMatchStats("21416434-4717-4966-9902-af7097469f74");
        Console.WriteLine("You have stats.");

        var academyData = await client.AcademyGetContent();
        Console.WriteLine("Got academy data.");         

        // Test getting the season data.
        var seasonData = await client.GameCmsGetSeasonRewardTrack(
            "Seasons/Season7.json",
            localClearance);
        Console.WriteLine("Got season data.");

        // Try getting skill qualifications with SkillGetMatchPlayerResult.
        List<string> sampleXuids = "xuid(2533274793272155),xuid(2533274814715980),xuid(2533274855333605),xuid(2535435749594170),xuid(2535448099228893),xuid(2535457135464780),xuid(2535466738529606),xuid(2535472868898775)".Split(',').ToList();
        var performanceData = (await client.SkillGetMatchPlayerResult("ad6a3d46-9320-44ee-94cd-c5cb39c7aedd", sampleXuids)).Result;
        Console.WriteLine("Got player performance data.");

        // Get an example image and store it locally.
        var imageData = (await client.GameCmsGetImage("progression/inventory/armor/gloves/003-001-olympus-8e7c9dff-sm.png")).Result;
        Console.WriteLine("Got image data.");
        if (imageData != null)
        {
            System.IO.File.WriteAllBytes("image.png", imageData);
            Console.WriteLine("Wrote sample image to file.");
        }
        else
        {
            Console.WriteLine("Image could not be written. There was an error.");
        }

        // Get bot customization data
        var botCustomization = (await client.AcademyGetBotCustomization(localClearance)).Result;
        if (botCustomization != null)
        {
            Console.WriteLine("Got but customization data.");
        }
        else
        {
            Console.WriteLine("Could not get bot customization data.");
        }

        // Get currency data
        var currencyData = (await client.GameCmsGetCurrency("currency/currencies/cr.json", localClearance)).Result;
        if (currencyData != null)
        {
            Console.WriteLine("Got currency data.");
        }
        else
        {
            Console.WriteLine("Could not get currency data.");
        }

        // Get reward data.
        var rewardData = (await client.EconomyGetAwardedRewards("xuid(2533274855333605)", "Challenges-35a86ae3-017c-4b5a-b633-b2802a770e0a")).Result;
        if (rewardData != null)
        {
            Console.WriteLine("Got reward data.");
        }
        else
        {
            Console.WriteLine("Could not get reward data.");
        }

        var matchData = await client.StatsGetMatchHistory("xuid(2533274855333605)", 0, 12, MatchType.All);
            
        var data = matchData.Result;
        if (data != null)
        {
            Console.WriteLine("Got match history data.");
        }
        else
        {
            Console.WriteLine("Could not get match history data.");
        }

        Console.ReadLine();
    }

    private static OAuthToken RequestNewToken(string url, XboxAuthenticationClient manager, ClientConfiguration clientConfig)
    {
        Console.WriteLine("Provide account authorization and grab the code from the URL:");
        Console.WriteLine(url);

        Console.WriteLine("Your code:");
        var code = Console.ReadLine();
        var currentOAuthToken = new OAuthToken();

        // If no local token file exists, request a new set of tokens.
        Task.Run(async () =>
        {
            currentOAuthToken = await manager.RequestOAuthToken(clientConfig.ClientId, code, clientConfig.RedirectUrl, clientConfig.ClientSecret);
            if (currentOAuthToken != null)
            {
                var storeTokenResult = StoreTokens(currentOAuthToken, "tokens.json");
                if (storeTokenResult)
                {
                    Console.WriteLine("Stored the tokens locally.");
                }
                else
                {
                    Console.WriteLine("There was an issue storing tokens locally. A new token will be requested on the next run.");
                }
            }
            else
            {
                Console.WriteLine("No token was obtained. There is no valid token to be used right now.");
            }
        }).GetAwaiter().GetResult();

        return currentOAuthToken;
    }

    private static bool StoreTokens(OAuthToken token, string path)
    {
        string json = JsonSerializer.Serialize(token);
        try
        {
            System.IO.File.WriteAllText(path, json);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
