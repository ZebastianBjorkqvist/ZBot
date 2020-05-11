using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Zbot.Models;
using ZBot.Modules;
using ZBot.Services;

namespace ZBot
{
    public class CurrentGameInfoModule : ModuleBase
    {
        private readonly RiotApiRequests _apiRequest;

        public CurrentGameInfoModule(RiotApiRequests apiRequest)
        {
            _apiRequest = apiRequest;
        }

        [Command("game")]
        [Summary("Gets info on users current active league game")]
        public async Task ActiveGameInfo([Remainder] [Summary("Summoner name")] string summonerName)
        {
            var summoner = await _apiRequest.GetSummoner(summonerName);
            var match = await _apiRequest.GetMatch(summonerName);

            if (match.Status?.Status_code == "404") //When you try to get a match of a summoner thats not in a game it returns 404
            {
                await ReplyAsync($"{summoner.Name} is not in a game");
                return;
            }
  

            var embedBuilder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Title = $"{summoner.Name} is in a game and here's the info"

            };

            embedBuilder.AddField("Gamemode", match.GameMode);


            var team1 = " ";
            var team2 = " ";

            foreach (var p in match.Participants)
            {
                if (p.TeamId == 100)
                    team1 += p.SummonerName + "\n";
                else
                    team2 += p.SummonerName + "\n";
            }

            embedBuilder.AddField("Blue team", team1, true);
            embedBuilder.AddField("Red team", team2, true);


            var gameStartTime = DateTimeOffset.FromUnixTimeMilliseconds(match.GameStartTime);
            var gameTimeSpan = DateTime.Now - gameStartTime;
            var mins = gameTimeSpan.Minutes + " min" + (gameTimeSpan.Minutes != 1 ? "s" : "") + " and ";
            var secs = gameTimeSpan.Seconds + " sec" + (gameTimeSpan.Seconds != 1 ? "s" : "");

            embedBuilder.AddField("Length", $"{mins} {secs}");


            await ReplyAsync("", false, embedBuilder.Build());

        }
    }
}


