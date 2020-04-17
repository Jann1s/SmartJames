using JamesCore.News;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace JamesCore.Voice
{
    class NewsVoice : ASpeech, ISpeech
    {
        private ANews newsService;

        public NewsVoice(SpeechConfig speechConfig, Config.Voice voiceConfig, News.ANews newsService) : base(speechConfig, voiceConfig)
        {
            this.newsService = newsService;
            this.newsService.NewsItems.CollectionChanged += UpdateVoiceText;
            ProcessItems(newsService.NewsItems);
        }

        private void UpdateVoiceText(object sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessItems(e.NewItems);
        }

        private void ProcessItems(System.Collections.IList newsItems)
        {
            string voiceText = voiceConfig.newsVoice.headline + " ";

            for (int i = 0; i < newsItems.Count; i++)
            {
                NewsItem newsItem = (NewsItem)newsItems[i];

                if (i > 0)
                {
                    voiceText += " " + voiceConfig.newsVoice.articleSeperator + " ";
                }

                voiceText += newsItem.title + " ... " + newsItem.content;

                voiceTexts.Add(voiceText);
                voiceText = "";
            }
        }
    }
}

