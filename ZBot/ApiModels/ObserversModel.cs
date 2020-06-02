using Newtonsoft.Json;

namespace Zbot.Models
{
    public class ObserversModel
    {
        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }
    }
}
