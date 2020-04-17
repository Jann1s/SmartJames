using System;

namespace JamesCore.Config
{
    public class JamesConfig
    {
        public Weather weather { get; set; }
        public News news { get; set; }
        public Voice voice { get; set; }
    }

    public class Weather
    {
        public string locationName { get; set; }
        public int updateInterval { get; set; }
        public WeatherService[] services { get; set; }
    }

    public class WeatherService
    {
        public string name { get; set; }
        public bool active { get; set; }
        public Uri url { get; set; }
        public string apiKey { get; set; }
        public string lang { get; set; }
        public string units { get; set; }
        public Location location { get; set; }
    }

    public class Location
    {
        public float langitude { get; set; }
        public float longitude { get; set; }
    }

    public class News
    {
        public int updateInterval { get; set; }
        public int maxItems { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string provider { get; set; }
        public string url { get; set; }
        public string parentContainerClass { get; set; }
        public string textContainerTag { get; set; }
    }

    public class Voice
    {
        public Provider[] provider { get; set; }
        public string weather { get; set; }
        public Newsvoice newsVoice { get; set; }
        public string[] voiceOrder { get; set; }
    }

    public class Newsvoice
    {
        public string headline { get; set; }
        public string articleSeperator { get; set; }
    }

    public class Provider
    {
        public string name { get; set; }
        public bool active { get; set; }
        public string subscriptionKey { get; set; }
        public string serverRegion { get; set; }
        public string lang { get; set; }
    }
}
