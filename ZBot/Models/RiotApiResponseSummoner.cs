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
}



