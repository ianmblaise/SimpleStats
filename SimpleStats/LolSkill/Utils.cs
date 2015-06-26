using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SimpleStats.Pages;
using static SimpleStats.LolSkill.AllTags;

namespace SimpleStats.LolSkill
{
    public class Utils
    {
        public static string HtmlPageString { get; private set; }
        public static List<string[]> GetNamesAndChampion(string playerName)
        {
            var client = new WebClient();
            var allNames = new List<string[]>();

            var log = Lookup.LookupPage;
            log.WriteToLog($"Downloading html page for {playerName}");
            HtmlPageString = client.DownloadString("http://www.lolskill.net/game/NA/" + playerName);
            var stopChecking = false;

            var teamData = GetStringInBetween(Tags["game.blueTeam"], HtmlPageString);
            teamData += GetStringInBetween(Tags["game.redTeam"], HtmlPageString);

            if (!IsInActiveGame(HtmlPageString) || teamData.Length < 1)
            {
                return null;
            }

            for(int i = 0; i < 10 && stopChecking == false; i++)
            {
                var bluePlayerName = GetStringInBetween(Tags["game.player"], teamData);
                teamData = teamData.Substring(teamData.IndexOf(bluePlayerName, StringComparison.Ordinal));
                var bluePlayerChampion = GetStringInBetween(Tags["game.player.champion"], teamData);

                if (bluePlayerName.Length > 0)
                {
                    allNames.Add(new [] { Uri.UnescapeDataString(bluePlayerName), bluePlayerChampion });
                    continue;

                }
                stopChecking = true;
            }
            return allNames;
        }

        public static bool IsInActiveGame(string htmlString)
        {
            var activeGameString = GetStringInBetween(Tags["game.noActiveGame"], htmlString);
            return !activeGameString.Contains("No Active Game");
        }

        public static string GetGameType()
        {
            var headers = GetStringInBetween(Tags["game.headerBox"], HtmlPageString);
            var name = GetStringInBetween(Tags["game.headerBox.name"], headers);
            var gameType = GetStringInBetween(Tags["game.headerBox.gameType"], headers);

            Console.WriteLine(name + @" playing " + gameType);
            return gameType;
        }

        public static Stats GetStatSummary(string playerName)
        {
            if(string.IsNullOrEmpty(playerName) || string.IsNullOrWhiteSpace(playerName))
            {
                return null;
            }

            WebClient client = new WebClient();
            var stats = new Stats();
            string htmlString = string.Empty;
            htmlString = client.DownloadString("http://www.lolskill.net/summoner/NA/" + playerName);


            var solo5V5Container = GetStringInBetween(Tags["summary.solo5v5"], htmlString);
            var tier = GetStringInBetween(Tags["summary.solo5v5.tier"], solo5V5Container);
            if (tier.Contains("Unranked"))
            {
                stats.Name = playerName;
                stats.Division = "Unranked";
                return stats;
            }

            var leaguePoints = GetStringInBetween(Tags["summary.solo5v5.leaguepoints"], solo5V5Container).TrimEnd(new [] {'L','e','a', 'g', 'u', 'e', 'P', 'o', 'i', 'n','t','s', ' '});
            var kdaString = GetStringInBetween(Tags["summary.solo5v5.kda"], solo5V5Container);
            string wins = GetStringInBetween(Tags["summary.solo5v5.wins"], solo5V5Container);
            string losses = GetStringInBetween(Tags["summary.solo5v5.losses"], solo5V5Container);
            var name = playerName;

            if(string.IsNullOrEmpty(solo5V5Container))
            {
                return null;
            }
            
            stats.LeaguePoints = int.Parse(leaguePoints);
            stats.Division = tier;

            int parsedWins;
            var winsParsed = int.TryParse(wins, out parsedWins);
            stats.Wins = winsParsed ? parsedWins : 0;

            int parsedLosses;
            var lossesParsed = int.TryParse(losses, out parsedLosses);
            stats.Losses = lossesParsed ? parsedLosses : 0;      
           
            var kda = kdaString.Substring(0, 3);
            var kdaAverages = kdaString.Substring(kdaString.IndexOf("&ndash;", StringComparison.Ordinal) + 8);
            var kdaStatString = $"{kda} KD | {kdaAverages} Per Game";

            stats.Kda = kdaStatString;

            stats.Name = name;

            Lookup.LookupPage.WriteToLog(stats.ToString());

           
            return stats;
        }
                 
        public static string GetStringInBetween(string[] tags, string bodyWithTags)
        {
            int startIndex = bodyWithTags.IndexOf(tags[0], StringComparison.Ordinal);

            if (startIndex < 0)
            {
                return string.Empty;
            }

            bodyWithTags = bodyWithTags.Substring(startIndex + tags[0].Length);
            int endIndex = bodyWithTags.IndexOf(tags[1], StringComparison.Ordinal);
            if (endIndex < 0)
            {
                return "";
            }
            string result = bodyWithTags.Substring(0, endIndex);
            if (result.Length < 1)
            {
                result = " ";
            }
            return result;
        }
    }
}
