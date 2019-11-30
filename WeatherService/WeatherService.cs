using System;
using System.Web;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherService.Entities;
using System.Collections.Specialized;

namespace WeatherService
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherOptions _options;

        public WeatherService(WeatherOptions options)
        {
            _httpClient = new HttpClient();

            _options = options ?? throw new ArgumentNullException(nameof(options));
            _options.Validate();
        }

        public Task<WeatherForecast> GetWeatherForecastAsync(string city)
        {
            var parameters = new NameValueCollection { { "q", city } };
            return RequestAsync<WeatherForecast>(parameters);
        }

        public Task<WeatherForecast> GetWeatherForecastAsync(int cityId)
        {
            var parameters = new NameValueCollection { { "id", cityId.ToString() } };
            return RequestAsync<WeatherForecast>(parameters);
        }

        private Uri BuildRequestUri(NameValueCollection queryParameters = null, string version = "2.5", string type = "weather")
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            if (queryParameters != null)
            {
                parameters.Add(queryParameters);
            }

            parameters.Add("APPID", _options.ApiKey);

            var query = parameters.ToString();
            var requestUri = new Uri(_options.BaseAddress.ToString() + version + "/" + type);
            var builder = new UriBuilder(requestUri) { Query = query };

            return builder.Uri;
        }

        private async Task<TEntity> RequestAsync<TEntity>(NameValueCollection queryParameters)
        {
            var requestUri = BuildRequestUri(queryParameters);
            var result = await SendAsync<TEntity>(HttpMethod.Get, requestUri);

            if (result == default)
            {
                return result;
            }

            return result;
        }

        private async Task<TEntity> SendAsync<TEntity>(HttpMethod httpMethod, Uri uri)
        {
            using (var request = new HttpRequestMessage(httpMethod, uri))
            {
                using (var response = await _httpClient.SendAsync(request))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return default;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        var messageBuilder = new StringBuilder();
                        messageBuilder.Append("OpenWeatherMap Request Error:");
                        messageBuilder.AppendFormat("\n- Got status {0} ({1}), expected: 2xx.", response.StatusCode, (int)response.StatusCode);
                        messageBuilder.AppendFormat("\n- Request Status Line: {0} {1}.", httpMethod.Method, uri);
                        messageBuilder.Append("\n- Response Headers:");

                        foreach (var header in response.Headers)
                        {
                            messageBuilder.AppendFormat("\n    - {0}:\t{1}", header.Key, string.Join(", ", header.Value));
                        }

                        messageBuilder.AppendFormat("\n- Response Content: {0}", content);
                        throw new InvalidOperationException(messageBuilder.ToString());
                    }

                    return JsonConvert.DeserializeObject<TEntity>(content);
                }
            }
        }

    }
}