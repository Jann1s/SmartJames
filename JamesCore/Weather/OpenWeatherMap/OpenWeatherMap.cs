using JamesCore.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamesCore.Weather.OpenWeatherMap
{
    class OpenWeatherMap : AWeather
    {
        public OpenWeatherMap(WeatherService service) : base(service)
        {
            TriggerWeather();
        }

        protected override async void TriggerWeather()
        {
            string url = ServiceConfig.url + "?APPID=" + ServiceConfig.apiKey + "&lat=" + ServiceConfig.location.langitude + "&lon=" + ServiceConfig.location.longitude + "&lang=" + ServiceConfig.lang + "&units=" + ServiceConfig.units;
            string message = await HttpClient.GetStringAsync(url).ConfigureAwait(true);
            OpenWeatherMapResponse response = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(message);
            WeatherData = new WeatherData("Twist", response.main.temp);
            TriggerWeatherUpdated();
        }
    }
}
