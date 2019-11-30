using WeatherService;
using System.Threading.Tasks;

namespace WeatherConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WeatherOptions options = new WeatherOptions();
            options.ApiKey = "enter_weather_api_key";

            var service = new WeatherService.WeatherService(options);
            var weather = await service.GetWeatherForecastAsync(2643741); //2643741=City of London
            var weather1 = await service.GetWeatherForecastAsync("Paris"); //2988507=Paris
            var weather2 = await service.GetWeatherForecastAsync(2964574); //2964574=Dublin
        }
    }
}
