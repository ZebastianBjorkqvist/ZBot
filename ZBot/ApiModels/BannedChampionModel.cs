using Newtonsoft.Json;

namespace Zbot.Models
{
    public class BannedChampionModel
    {
        [JsonProperty("championId")]
        public long ChampionId { get; set; }

        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("pickTurn")]
        public long PickTurn { get; set; }
    }
}
