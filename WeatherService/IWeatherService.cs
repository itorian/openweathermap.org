using System.Threading.Tasks;
using WeatherService.Entities;

namespace WeatherService
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetWeatherForecastAsync(string city);
        Task<WeatherForecast> GetWeatherForecastAsync(int cityId);
    }
}