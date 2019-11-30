using Newtonsoft.Json;

namespace WeatherService.Entities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}