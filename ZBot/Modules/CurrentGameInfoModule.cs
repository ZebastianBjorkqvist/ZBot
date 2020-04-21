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
            
            await ReplyAsync($"Current activity ");
        }
    }
 }


