using JamesCore.Weather;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Specialized;
using System.Globalization;

namespace JamesCore.Voice
{
    class WeatherVoice : ASpeech, ISpeech
    {
        private AWeather weatherService;

        public WeatherVoice(SpeechConfig speechConfig, Config.Voice voiceConfig, AWeather weatherService) : base(speechConfig, voiceConfig)
        {
            this.weatherService = weatherService;
            this.weatherService.WeatherUpdated += UpdateVoiceText;
            UpdateVoiceText(this, EventArgs.Empty);
        }

        private void UpdateVoiceText(object sender, EventArgs e)
        {
            WeatherData weatherData = weatherService.WeatherData;
            string voiceText = voiceConfig.weather;
            voiceText = voiceText.Replace("${temparatur}", Math.Round(weatherData.Temperature).ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal).Replace("${location}", weatherData.LocationName.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal);
            voiceTexts.Add(voiceText);
        }
    }
}
