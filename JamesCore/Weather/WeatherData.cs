using System;
using System.Collections.Generic;
using System.Text;

namespace JamesCore.Weather
{
    public class WeatherData
    {
        public string LocationName { get; }
        public float Temperature { get; }

        public WeatherData(string locationName, float temperature)
        {
            this.LocationName = locationName;
            this.Temperature = temperature;
        }
    }
}
