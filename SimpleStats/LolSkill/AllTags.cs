using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SimpleStats.LolSkill
{
    public static class AllTags
    {
        public static Dictionary<string, string[]> Tags => new Dictionary<string, string[]>
        {
            { "summary.solo5v5.tier", new[] { "<p class=\"tier\">", "</p>" }},
            { "summary.solo5v5.leaguepoints", new[] { "<p class=\"leaguepoints\">", "</p>" }},
            { "summary.solo5v5.kda", new[] { "<p class=\"kda\">KDA: ", "</p>" }},
            { "summary.solo5v5.wins", new[] { "<span class=\"wins\">", " W</span>" }},
            { "summary.solo5v5.losses", new[] { "<span class=\"losses\">", " L</span>" }},
            { "summary.solo5v5", new[] {
                "<div class=\"container ranking\" id=\"rankedSolo\">",
                "<div class=\"container ranking\" id=\"ranked5v5\">" }},
            { "summary.name", new[] { "<title>", " &n" }},
            { "game.blueTeam", new[] { "<div class=\"wrapInner\">", "<hr>" }},
            { "game.redTeam", new[] { "<div class=\"teaminfo redteam\">", "<footer>" }},
            { "game.player", new [] { "<div class=\"summonername\"><a href=\"summoner/NA/", "\">" }},
            { "game.player.champion", new []{ "<a href=\"champion/", "\">" }},
            { "game.noActiveGame", new [] { "<div class=\"head\">" , "</h3>" }},
            { "game.headerBox", new [] { "<div class=\"box\">", "</div>" }},
            { "game.headerBox.name", new []{ "<h2>", "</h2>" }},
            { "game.headerBox.gameType", new []{ "<h3>", " &" }}
        };
    }
}
