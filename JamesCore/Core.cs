using System;
using System.Globalization;
using System.IO;
using Google.Cloud.TextToSpeech.V1;
using JamesCore.Config;
using JamesCore.News;
using JamesCore.News.DeutscheWelle;
using JamesCore.Voice;
using JamesCore.Weather;
using JamesCore.Weather.DarkSky;
using JamesCore.Weather.OpenWeatherMap;
using Microsoft.CognitiveServices.Speech;
using Newtonsoft.Json;

namespace JamesCore
{
    public class Core
    {
        private JamesConfig jamesConfig;
        private AWeather weatherService;
        private ANews newsService;
        private SpeechConfig speechConfig;

        public Core()
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            using (StreamReader reader = new StreamReader("config.json"))
            {
                jamesConfig = JsonConvert.DeserializeObject<JamesConfig>(reader.ReadToEnd());
            }

            foreach (Provider voice in jamesConfig.voice.provider)
            {
                if (voice.active)
                {
                    if (voice.name == "azure")
                    {
                        speechConfig = SpeechConfig.FromSubscription(voice.subscriptionKey, voice.serverRegion);
                        speechConfig.SpeechSynthesisLanguage = voice.lang;
                        speechConfig.SpeechSynthesisVoiceName = "de-DE-Stefan-Apollo";
                    }
                    else if (voice.name == "google")
                    {
                        //TextToSpeechClient textToSpeechClient = TextToSpeechClient.Create();
                    }
                }
            }

            foreach (WeatherService service in jamesConfig.weather.services)
            {
                if (service.active)
                {
                    switch (service.name)
                    {
                        case "DarkSy":
                            weatherService = new DarkSky(service);
                            break;
                        case "OpenWeatherMap":
                            weatherService = new OpenWeatherMap(service);
                            break;
                    }
                }
            }

            foreach (Item news in jamesConfig.news.items)
            {
                switch (news.provider)
                {
                    case "DeutscheWelle":
                        newsService = new DeutscheWelle(news);
                        break;
                }
            }
        }

        public void TriggerWeatherVoice()
        {
            ASpeech aSpeech = new WeatherVoice(speechConfig, jamesConfig.voice, weatherService);
            aSpeech.Speak();
        }

        public void TriggerNewsVoice()
        {
            ASpeech aSpeech = new NewsVoice(speechConfig, jamesConfig.voice, newsService);
            aSpeech.Speak();
        }
    }
}
