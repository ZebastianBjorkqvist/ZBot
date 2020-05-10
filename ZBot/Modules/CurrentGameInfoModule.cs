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
            var summoner = await _apiRequest.GetSummoner<RiotApiResponseSummoner>(summonerName);
            var match = await _apiRequest.GetMatch<LeagueMatch>(summonerName);
            var embedBuilder = new EmbedBuilder()   {  Color = new Color(114, 137, 218) };

            if (match.Status.Message == "404") //When you try to get a match of a summoner thats not in a game it returns 404
            {
                embedBuilder.WithDescription($"{summoner.Name} is not in a game");
                await ReplyAsync("", false, embedBuilder.Build());
                return;
            }

            embedBuilder.WithDescription($"{summoner.Name} is in a game and here's the info");

            
            embedBuilder.AddField("Gamemode", match.GameMode);
            embedBuilder.AddField("Participants", match.Participants);
            embedBuilder.AddField("Length", match.GameLength);
           
            await ReplyAsync("", false, embedBuilder.Build());
        }
    }
 }


