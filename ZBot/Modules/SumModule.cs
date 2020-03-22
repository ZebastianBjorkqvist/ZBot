using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;


namespace ZBot.Modules
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class RiotApiResponseRank
    {
        public string QueueType { get; set; }
        public string SummonerName { get; set; }
        public bool HotStreak { get; set; }
        public int Wins { get; set; }
        public bool Veteran { get; set; }
        public int Losses { get; set; }
        public string Rank { get; set; }
        public string Tier { get; set; }
        public bool Inactive { get; set; }
        public bool FreshBlood { get; set; }
        public string LeagueId { get; set; }
        public string SummonerId { get; set; }
        public int LeaguePoints { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class RiotApiResponseSummoner
    {
        public int ProfileIconId { get; set; }
        public string Name { get; set; }
        public string Puuid { get; set; }
        public int SummonerLevel { get; set; }
        public string AccountId { get; set; }
        public string Id { get; set; }
        public long RevisionDate { get; set; }
    }

    public static class QueueType
    {
        public static readonly string Solo5v5 = "RANKED_SOLO_5x5";
    }

    public class SumModule : ModuleBase
    {
        private readonly RiotApiHandler _apiHandler;

        public SumModule(RiotApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        [Command("sum")]
        [Summary("Gets summoner level and rank.")]
        public async Task GetSumLvlAndRank([Remainder] [Summary("Summoner name")] string summonerName)
        {
            var escapedName = Uri.EscapeUriString(summonerName); //Need to escape incase it has a space in the name

            string urlSummoner = $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{escapedName}"; //Url to get summoner info with summonername

            var summoner = await _apiHandler.ApiRequest<RiotApiResponseSummoner>(urlSummoner);

            string urlRank = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-summoner/{summoner.Id}"; //Url to get ranked information of summoner. It only accepts summoner id
            
            var ranked = await _apiHandler.ApiRequest<RiotApiResponseRank[]>(urlRank);
            
            /*Because tha api is random in wich ranked que json (Flex or Solo) is sent first
             * we need to find the one where queueType is solo*/
            if ((ranked.FirstOrDefault(x => x.QueueType == QueueType.Solo5v5) is RiotApiResponseRank summonerRanked))
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



