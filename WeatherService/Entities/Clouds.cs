using Newtonsoft.Json;

namespace WeatherService.Entities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}