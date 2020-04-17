using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamesCore.Voice
{
    class ASpeech
    {
        protected readonly Config.Voice voiceConfig;
        protected readonly SpeechConfig speechConfig;
        protected List<string> voiceTexts = new List<string>();

        public ASpeech(SpeechConfig speechConfig, Config.Voice voiceConfig)
        {
            this.speechConfig = speechConfig;
            this.voiceConfig = voiceConfig;
        }

        public async void Speak()
        {
            using var synthesizer = new SpeechSynthesizer(speechConfig);

            foreach (string text in voiceTexts)
            {
                using var result = await synthesizer.SpeakTextAsync(text).ConfigureAwait(true);
                if (result.Reason == ResultReason.Canceled)
                {
                    //File.WriteAllBytes("test.wav", result.AudioData);
                }
            }

            voiceTexts.Clear();
        }
    }
}
