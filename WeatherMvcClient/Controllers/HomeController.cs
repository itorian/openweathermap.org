using System;
using System.IO;
using System.Linq;
using WeatherService;
using System.Diagnostics;
using System.Threading.Tasks;
using WeatherMvcClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace WeatherMvcClient.Controllers
{
    public class HomeController : Controller
    {
        private IHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;

        public HomeController(IHostEnvironment hostEnvironment, ILogger<HomeController> logger, IWeatherService weatherService)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(List<IFormFile> files)
        {
            var weatherUploadedCities = new List<WeatherUploadedFileModel>();

            long size = files.Sum(f => f.Length);
            var filePaths = new List<string>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            var line = await reader.ReadLineAsync();

                            List<string> words = line.Split('=', StringSplitOptions.RemoveEmptyEntries).Where(i => i.Length != 0).ToList();

                            weatherUploadedCities.Add(new WeatherUploadedFileModel
                            {
                                CityId = Convert.ToInt32(words[0]),
                                City = words[1]
                            });
                        }
                    }
                }
            }

            foreach (var item in weatherUploadedCities)
            {
                var weatherByCityId = await _weatherService.GetWeatherForecastAsync(item.CityId);
                //var weatherByCityName = await _weatherService.GetWeatherForecastAsync(item.City);

                var json = JsonSerializer.Serialize(weatherByCityId);

                var dir = _hostEnvironment.ContentRootPath + "\\WeatherHistoricalData\\" + DateTime.Now.ToString("dd-MM-yyyy");
                var _file = Path.Combine(dir, item.CityId + "=" + item.City + ".txt");

                Directory.CreateDirectory(dir);
                System.IO.File.WriteAllText(_file, json);
            }

            return Redirect("~/Home/UploadSuccess");
        }

        public IActionResult UploadSuccess()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
