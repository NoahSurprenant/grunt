using OpenSpartan.Grunt.Core;
using OpenSpartan.Grunt.Librarian.Models;
using OpenSpartan.Grunt.Models.HaloInfinite.ApiIngress;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSpartan.Grunt.Librarian;

class Program
{
    private static string GetCallerFilePath([CallerFilePath] string path = null) => path;

    private static string GetEndpointsFilePath()
    {
        var thisPath = GetCallerFilePath();
        var dir = Path.GetDirectoryName(thisPath);
        return Path.Combine(dir, "endpoints.json");
    }

    private static async Task<ApiSettingsContainer> GetContainerAsync(bool useCachedEndpoints = true)
    {
        var endpointsFilePath = GetEndpointsFilePath();

        if (useCachedEndpoints && File.Exists(endpointsFilePath))
        {
            var color = Console.ForegroundColor;
            try
            {
                return JsonSerializer.Deserialize<ApiSettingsContainer>(await File.ReadAllTextAsync(endpointsFilePath));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to deserialize: " + ex.Message);
                Console.WriteLine("Deleting " + endpointsFilePath);
                File.Delete(endpointsFilePath);
                Console.WriteLine("Falling back to infinite client");
            }
            finally
            {
                Console.ForegroundColor = color;
            }
        }

        HaloInfiniteClient client = new(string.Empty, string.Empty);

        // Get the API endpoint data.
        var resultContainer = await client.GetApiSettingsContainer();
        var container = resultContainer.Result;

        if (container is null)
        {
            Console.WriteLine("Result was null. Error Message:");
            Console.WriteLine(resultContainer.Error.Message);
            return null;
        }

        var apiSettingsContainerString = JsonSerializer.Serialize(container, options: new()
        {
            WriteIndented = true,
        }
        );

        File.WriteAllText(GetEndpointsFilePath(), apiSettingsContainerString);

        return container;
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Grunt Librarian - Halo Infinite API Indexer");
        Console.WriteLine("Developed by Den Delimarsky in 2022. Part of https://gruntapi.com");

        var container = await GetContainerAsync();

        if (container is null)
            return;

        List<ExportableFunction> functions = new();

        foreach(var endpoint in container.Endpoints)
        {
            ApiAuthority endpointAuthority = (from c in container.Authorities where string.Equals(c.Key, endpoint.Value.AuthorityId, StringComparison.InvariantCultureIgnoreCase) select c).First().Value;
            var endpointNamePieces = endpoint.Key.Split('_');
            //func.Name = endpointNamePieces.Last();

            ExportableFunction func = new();
            func.Name = endpoint.Key.Replace("_", "");
            func.EndpointPath = endpoint.Value.Path;
            func.EndpointId = endpoint.Key;
            func.AuthorityHost = endpointAuthority.Hostname;
            func.AuthorityPort = endpointAuthority.Port;
            func.QueryString = endpoint.Value.QueryString;
            func.RequiresClearance = endpoint.Value.ClearanceAware;
            func.RequiresSpartanToken = endpointAuthority.AuthenticationMethods.Contains(15);
                
            switch(func.Name)
            {
                case string s when s.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase):
                    func.Method = HttpMethod.Get;
                    break;
                case string s when s.StartsWith("Post", StringComparison.InvariantCultureIgnoreCase):
                    func.Method = HttpMethod.Post;
                    break;
                case string s when s.StartsWith("Delete", StringComparison.InvariantCultureIgnoreCase):
                    func.Method = HttpMethod.Delete;
                    break;
                case string s when s.StartsWith("Upload", StringComparison.InvariantCultureIgnoreCase):
                    func.Method = HttpMethod.Post;
                    break;
                case string s when s.StartsWith("Put", StringComparison.InvariantCultureIgnoreCase):
                    func.Method = HttpMethod.Put;
                    break;
                default:
                    func.Method = HttpMethod.Get;
                    func.NeedsIntervention = true;
                    break;
            }

            func.Class = string.Concat(endpointNamePieces.Take(endpointNamePieces.Length - 1));
            functions.Add(func);
        }

        var functionStub =
        @"public async Task<string> {0} ({1}) {{
                var response = await ExecuteAPIRequest($""https://{2}:{3}{4}{5}"",
                                        HttpMethod.{6},
                                        {7},
                                        {8},
                                        GlobalConstants.HALO_WAYPOINT_USER_AGENT);

                if (!string.IsNullOrEmpty(response))
                {{
                    return response;
                }}
                else
                {{
                    return string.Empty;
                }}
            }}";

        var entityRegex = new Regex(@"\{(.*?)\}");

        foreach (var func in functions)
        {
            var endpointCombo = string.Concat(func.EndpointPath, func.QueryString);
            var functionParameters = entityRegex.Matches(endpointCombo);
            var arguments = string.Empty;

            if (functionParameters.Count > 0)
            {
                foreach (Match match in functionParameters)
                {
                    arguments += "string " + match.Groups[1].Value.ToString() + ", ";
                }
                arguments = arguments.Trim().TrimEnd(',');
            }

            var functionCode = string.Format(functionStub,
                                                func.Name,                                                                                     
                                                arguments,                                                                                     
                                                func.AuthorityHost,                                                                               
                                                func.AuthorityPort,                                                                            
                                                func.EndpointPath,                                                                 
                                                func.QueryString,                                                                              
                                                Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(func.Method.ToString().ToLower()),    
                                                func.RequiresSpartanToken.ToString().ToLower(),                                                
                                                func.RequiresClearance.ToString().ToLower()                                                    
                                                );
                
            if (func.NeedsIntervention)
            {
                functionCode = "//TODO: This function requires manual invtervention/checks.\n" + functionCode;
            }

            func.FunctionCode = functionCode;
        }

        var combinations = from f in functions
                            group f.FunctionCode by f.Class into g
                            select new { Class = g.Key, Functions = g.ToList() };

        foreach(var combo in combinations)
        {
            Console.WriteLine("//================================================");
            Console.WriteLine("// " + combo.Class);
            Console.WriteLine("//================================================");

            foreach(var function in combo.Functions)
            {
                Console.WriteLine(function);
                Console.WriteLine();
            }
        }
    }
}
