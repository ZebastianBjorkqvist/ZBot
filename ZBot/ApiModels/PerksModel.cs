using Newtonsoft.Json;

namespace Zbot.Models
{
    public class PerksModel
    {
        [JsonProperty("perkIds")]
        public long[] PerkIds { get; set; }

        [JsonProperty("perkStyle")]
        public long PerkStyle { get; set; }

        [JsonProperty("perkSubStyle")]
        public long PerkSubStyle { get; set; }
    }
}
