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
}



