using JamesCore.Config;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace JamesCore.Weather.DarkSky
{
    class DarkSky : AWeather
    {
        public DarkSky(WeatherService service) : base(service)
        {
            TriggerWeather();
        }

        protected override async void TriggerWeather()
        {
            string url = ServiceConfig.url + ServiceConfig.apiKey + "/" + ServiceConfig.location.langitude + "," + ServiceConfig.location.longitude + "?lang=" + ServiceConfig.lang + "&units=" + ServiceConfig.units;
            string message = await HttpClient.GetStringAsync(url).ConfigureAwait(true);
            DarkResponse response = JsonConvert.DeserializeObject<DarkResponse>(message);
            WeatherData = new WeatherData("Twist", response.currently.temperature);
            TriggerWeatherUpdated();
        }
    }
}
