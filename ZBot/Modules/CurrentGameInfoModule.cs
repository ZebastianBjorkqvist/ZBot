using Discord;
using Discord.Commands;
using System;
using System.IO;
using System.Threading.Tasks;
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
            var ranked = await _apiRequest.GetSummonerRank<RiotApiResponseRank[]>(summonerName);

            var embedBuilder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = $"{summoner.Name} is in a game and here's the info"
            };


            string embedFieldText = "";
            
            embedBuilder.AddField("test", embedFieldText);
            
            await ReplyAsync("", false, embedBuilder.Build());
        }
    }
 }


