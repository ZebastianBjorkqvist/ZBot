﻿using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZBot.Services;

namespace ZBot.Modules
{
    public class SumModule : ModuleBase
    {
        private readonly RiotApiRequests _apiRequest;

        public SumModule(RiotApiRequests apiRequest)
        {
            _apiRequest = apiRequest;
        }

        [Command("sum")]
        [Summary("Gets summoner level and rank.")]
        public async Task GetSumLvlAndRank([Remainder] [Summary("Summoner name")] string summonerName)
        {
            RiotApiResponseSummonerModel summoner = await _apiRequest.GetSummoner(summonerName);
            RiotApiResponseRankModel[] ranked = await _apiRequest.GetSummonerRank(summonerName);

            /*Because tha api is random in wich ranked que json (Flex or Solo) is sent first
             * we need to find the one where queueType is solo. Noone cares about flex*/
            if (ranked.FirstOrDefault(x => x.QueueType == QueueTypeModel.Solo5v5) is RiotApiResponseRankModel summonerRanked)
            {
                await ReplyAsync($"{summoner.Name} is level {summoner.SummonerLevel} and is {summonerRanked.Tier} {summonerRanked.Rank}");
            }
            else
            {
                await ReplyAsync($"{summoner.Name} is level {summoner.SummonerLevel} but has no rank");
            }
        }
    }
}



