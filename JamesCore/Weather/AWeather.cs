using JamesCore.Config;
using System;
using System.Net.Http;
using System.Threading;

namespace JamesCore.Weather
{
    public abstract class AWeather
    {
        public WeatherData WeatherData { get; protected set; }
        public event EventHandler WeatherUpdated;

        protected WeatherService ServiceConfig { get; }
        protected HttpClient HttpClient { get; } = new HttpClient();

        private Thread thread;

        public AWeather(WeatherService service)
        {
            ServiceConfig = service;
        }

        public void StartUpdate()
        {
            thread = new Thread(new ThreadStart(WeatherThread));
            thread.Start();
        }

        public void StopUpdate()
        {
            thread.Abort();
        }

        private void WeatherThread()
        {
            while (true)
            {
                Thread.Sleep(new TimeSpan(0, 30, 0)); //TODO: Thread Sleep Interval for news
                TriggerWeather();
            }
        }

        protected abstract void TriggerWeather();

        protected void TriggerWeatherUpdated() => WeatherUpdated?.Invoke(this, EventArgs.Empty);
    }
}
