using Newtonsoft.Json;

namespace WeatherService.Entities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class SysInformation
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("sunrise")]
        public int sunrise { get; set; }

        [JsonProperty("sunset")]
        public int sunset { get; set; }
    }
}
