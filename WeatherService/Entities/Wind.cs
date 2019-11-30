using Newtonsoft.Json;

namespace WeatherService.Entities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}