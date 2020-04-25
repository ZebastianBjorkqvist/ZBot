using Newtonsoft.Json;

namespace Zbot.Models
{
    public class BannedChampion
    {
        [JsonProperty("championId")]
        public long ChampionId { get; set; }

        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("pickTurn")]
        public long PickTurn { get; set; }
    }
}
