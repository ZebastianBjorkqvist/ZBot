using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace ZBot
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

    public class Commands : ModuleBase<SocketCommandContext>
    {
        private readonly RiotApiHandler Api = new RiotApiHandler();

        [Command("ping")]
        public async Task Ping() => await ReplyAsync("Pong");


        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo) => ReplyAsync(echo);


        [Command("sum")]
        [Summary("Gets summoner level and rank.")]
        public async Task GetSumLvlAndRank([Remainder] [Summary("Summoner name")] string name)
        {
            var escapedName = Uri.EscapeUriString(name);
            string tier = "";
            string rank = "";

            string urlSummoner = $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{escapedName}";

            var summoner = JsonConvert.DeserializeObject<RiotApiResponseSummoner>(await Api.ApiClient(urlSummoner));

            string urlRank = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-summoner/{summoner.Id}";

            var rankJson = await Api.ApiClient(urlRank);

            if (rankJson.Count() < 5)
            {
                await ReplyAsync($"{summoner.Name} is level {summoner.SummonerLevel} but has no rank");
                return;
            }

            var ranked = JsonConvert.DeserializeObject<RiotApiResponseRank[]>(rankJson);

            if ((ranked.FirstOrDefault(x => x.QueueType == QueueType.Solo5v5) is RiotApiResponseRank summonerRanked))
            {
                tier = summonerRanked.Tier;
                rank = summonerRanked.Rank;
            }


            await ReplyAsync($"{summoner.Name} is level {summoner.SummonerLevel} and is {tier} {rank}");

        }
    }
}

