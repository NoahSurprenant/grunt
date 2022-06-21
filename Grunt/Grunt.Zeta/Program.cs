﻿using Grunt.Authentication;
using Grunt.Core;
using Grunt.Models;
using Grunt.Models.HaloInfinite;
using Grunt.Util;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.IO;

namespace Grunt.Zeta
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationReader clientConfigReader = new();
            var clientConfig = clientConfigReader.ReadConfiguration<ClientConfiguration>("client.json");
            //var gruntConfig = clientConfigReader.ReadConfiguration<GruntConfiguration>("grunt.json");

            XboxAuthenticationManager manager = new();
            var url = manager.GenerateAuthUrl(clientConfig.ClientId, clientConfig.RedirectUrl);

            HaloAuthenticationClient haloAuthClient = new();

            Console.WriteLine("Provide account authorization and grab the code from the URL:");
            Console.WriteLine(url);

            Console.WriteLine("Your code:");
            var code = Console.ReadLine();

            var accessToken = string.Empty;

            var ticket = new XboxTicket();
            var haloTicket = new XboxTicket();
            var extendedTicket = new XboxTicket();

            var xblToken = string.Empty;
            var haloToken = new SpartanToken();

            Task.Run(async () =>
            {
                var tokens = await manager.RequestOAuthToken(clientConfig.ClientId, code, clientConfig.RedirectUrl, clientConfig.ClientSecret);
                accessToken = tokens.AccessToken;
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                ticket = await manager.RequestUserToken(accessToken);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                haloTicket = await manager.RequestXstsToken(ticket.Token);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                extendedTicket = await manager.RequestXstsToken(ticket.Token, false);
            }).GetAwaiter().GetResult();

            if (ticket != null)
            {
                xblToken = manager.GetXboxLiveV3Token(haloTicket.DisplayClaims.Xui[0].Uhs, haloTicket.Token);
            }

            Task.Run(async () =>
            {
                haloToken = await haloAuthClient.GetSpartanToken(haloTicket.Token);
                Console.WriteLine("Your Halo token:");
                Console.WriteLine(haloToken.Token);
            }).GetAwaiter().GetResult();

            HaloInfiniteClient client = new(haloToken.Token, "", extendedTicket.DisplayClaims.Xui[0].Xid);

            var xuidDict = new Dictionary<string, string>();

            using (var reader = new StreamReader(@"C:\ReportCard\xuids.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    xuidDict[values[0]] = values[1];
                }

            }

            var resultsDict = new Dictionary<string, List<PlayerSkillResultValue>>();
            var matchesDict = new Dictionary<string, List<string>>();

            // Try getting actual Halo Infinite data.
            Task.Run(async () =>
            {
                var matchIds = new List<string>();
                foreach (KeyValuePair<string, string> entry in xuidDict)
                {
                    var xuid = entry.Value;

                    var test = await client.StatsGetMatchHistory(xuid);

                    matchIds = new List<string>();

                    foreach (var r in test.Results)
                    {
                        matchIds.Add(r.MatchId);
                    }

                    var playerSkillResultList = new List<PlayerSkillResultValue>();

                    foreach (var id in matchIds)
                    {
                        playerSkillResultList.Add(await client.SkillGetMatchPlayerResult(id, xuid));
                        //Console.WriteLine(await client.SkillGetMatchPlayerResultString(id, xuid));
                    }
                    resultsDict[entry.Key] = playerSkillResultList;
                    matchesDict[entry.Key] = matchIds;
                    Console.WriteLine(entry.Key + " is done");
                }
                WriteAllPlayersSkillResultValue(resultsDict, matchesDict);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                var example = await client.SkillGetMatchResult("9bfd4d13-f632-4c8d-bf74-f888a909d604");
                Console.WriteLine(example);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                var example = await client.StatsGetMatchStats("9bfd4d13-f632-4c8d-bf74-f888a909d604");
                Console.WriteLine(example);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                var example = await client.StatsGetMatchHistory("xuid(2533274901904952)");
                Console.WriteLine(example);
            }).GetAwaiter().GetResult();


            Console.WriteLine("This is it.");
            Console.ReadLine();
        }

            public static void WriteAllPlayersSkillResultValue(Dictionary<string,List<PlayerSkillResultValue>> dict, Dictionary<string,List<string>> matchIds)
            {
                using (var workbook = new XLWorkbook())
                {
                    foreach (KeyValuePair<string, List<PlayerSkillResultValue>> entry in dict)
                    {
                        var worksheet = workbook.Worksheets.Add(entry.Key);
                        worksheet.Cell("A1").Value = "MatchID";
                        worksheet.Cell("B1").Value = "Kills - Actual";
                        worksheet.Cell("C1").Value = "Deaths - Actual";
                        worksheet.Cell("D1").Value = "Kills - Expected";
                        worksheet.Cell("E1").Value = "Deaths - Expected";
                        worksheet.Cell("F1").Value = "Kills - Std Dev";
                        worksheet.Cell("G1").Value = "Deaths - Std Dev";
                        worksheet.Cell("H1").Value = "PreMatchCSR";
                        worksheet.Cell("I1").Value = "PostMatchCSR";

                        var i = 2;
                        foreach (var p in entry.Value)
                        {
                            var result = p.Value[0].Result;
                            worksheet.Cell($"A{i}").Value = $"{matchIds[entry.Key][i-2]}";
                            worksheet.Cell($"B{i}").Value = $"{result.StatPerformances.Kills.Count}";
                            worksheet.Cell($"C{i}").Value = $"{result.StatPerformances.Deaths.Count}";
                            worksheet.Cell($"D{i}").Value = $"{result.StatPerformances.Kills.Expected}";
                            worksheet.Cell($"E{i}").Value = $"{result.StatPerformances.Deaths.Expected}";
                            worksheet.Cell($"F{i}").Value = $"{result.StatPerformances.Kills.StdDev}";
                            worksheet.Cell($"G{i}").Value = $"{result.StatPerformances.Deaths.StdDev}";
                            worksheet.Cell($"H{i}").Value = $"{result.RankRecap.PreMatchCsr.Value}";
                            worksheet.Cell($"I{i}").Value = $"{result.RankRecap.PostMatchCsr.Value}";
                            i += 1;
                        }
                        worksheet.Columns().AdjustToContents();
                    }
                workbook.SaveAs("Report Card.xlsx");
                }
            }

    }
}
