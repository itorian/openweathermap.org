using Newtonsoft.Json;

namespace WeatherService.Entities
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class Coordinate
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}
