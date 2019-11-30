# Weather file upload project
This project/solution contains .net core implementation of openweathermap.org, you will find 4 projects in the solution:
  - WeatherService - a .net core 3 class library wrapper for openweathermap.org REST API
  - WeatherConsoleClient - .net core 3 console client for WeatherService wrapper quick testing
  - WeatherMvcClient - .net core 3 MVC client which uses WeatherService wrapper for weather data. Using this project you can upload .txt file which will have list of cityid=cityname data and then based on cityid or cityname it generates .txt files with naming convension for historical analysis.

Here's the solution and its projects:-
![Logo](https://imgur.com/dqWFoB2.png)

# How to run console client ?
  - Open Program.cs file and replace "enter_weather_api_key"
  - Set this project as startup and click to run

# How to run mvc client ?
  - Open appsettings.Development.json file and replace "enter_weather_api_key"
  - Set this project as startup and click to run
  - You need a sample file to upload which you can find here 
  - This project creates folders and files inside "WeatherHistoricalData" on the root

Here's the output data and sample file to use:
![Logo](https://imgur.com/epN9hpD.png)

# Note: Test project is added but code coverage is not 100%