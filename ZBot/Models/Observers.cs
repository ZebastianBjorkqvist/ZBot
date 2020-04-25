using Newtonsoft.Json;

namespace Zbot.Models
{
    public class Observers
    {
        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }
    }
}
