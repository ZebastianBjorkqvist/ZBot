using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zbot.Models;
using ZBot.Modules;

namespace ZBot
{
    public class CurrentGameInfoModule : ModuleBase
    {
        //There is about 15 gamemodes most were temprorary but this Dictionary allows the bot to handle all of them.
        //I Have predefined the most common
        //TODO: It will almost never save any new data since it resets at reboot and it maybe resets each time you run the command
        private readonly Dictionary<string, string> _gamemodeMap = new Dictionary<string, string>
        {
            { "CLASSIC", "Classic Summoner's Rift" },
            { "ARAM", "ARAM" },
        };

        private readonly RiotApiRequests _apiRequest;

        public CurrentGameInfoModule(RiotApiRequests apiRequest)
        {
            _apiRequest = apiRequest;
        }

        [Command("game")]
        [Summary("Gets info on users current active league game")]
        public async Task ActiveGameInfo([Remainder] [Summary("Summoner name")] string summonerName)
        {
            RiotApiResponseSummonerModel summoner = await _apiRequest.GetSummoner(summonerName);
            LeagueMatchModel match = await _apiRequest.GetMatch(summonerName);

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


            if(!_gamemodeMap.ContainsKey(match.GameMode))
            {
                string titleCase = char.ToUpper(match.GameMode[0]) + match.GameMode.Substring(1).ToLower();
                _gamemodeMap.Add(match.GameMode, titleCase);
                Console.WriteLine($"Added {match.GameMode} to dict -> {titleCase}");
            }

            embedBuilder.AddField("Gamemode", _gamemodeMap[match.GameMode]);


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

            DateTimeOffset gameStartTime = DateTimeOffset.FromUnixTimeMilliseconds(match.GameStartTime);
            TimeSpan gameTimeSpan = DateTime.Now - gameStartTime;
            var mins = gameTimeSpan.Minutes + " min" + (gameTimeSpan.Minutes != 1 ? "s" : "");
            var secs = gameTimeSpan.Seconds + " sec" + (gameTimeSpan.Seconds != 1 ? "s" : "");

            embedBuilder.AddField("Length", $"{mins} and {secs}");

            await ReplyAsync("", false, embedBuilder.Build());
        }
    }
}


