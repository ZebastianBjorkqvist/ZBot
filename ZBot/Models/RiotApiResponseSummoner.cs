using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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



