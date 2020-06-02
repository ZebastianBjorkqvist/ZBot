using Newtonsoft.Json;

namespace Zbot.Models
{
    public class ParticipantModel
    {
        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("spell1Id")]
        public long Spell1Id { get; set; }

        [JsonProperty("spell2Id")]
        public long Spell2Id { get; set; }

        [JsonProperty("championId")]
        public long ChampionId { get; set; }

        [JsonProperty("profileIconId")]
        public long ProfileIconId { get; set; }

        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }

        [JsonProperty("bot")]
        public bool Bot { get; set; }

        [JsonProperty("summonerId")]
        public string SummonerId { get; set; }

        [JsonProperty("gameCustomizationObjects")]
        public object[] GameCustomizationObjects { get; set; }

        [JsonProperty("perks")]
        public PerksModel Perks { get; set; }
    }
}
