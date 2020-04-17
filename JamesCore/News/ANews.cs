using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace JamesCore.News
{
    public abstract class ANews
    {
        private Thread thread;
        protected Config.Item NewsConfig { get; }
        protected HttpClient HttpClient { get; } = new HttpClient();
        public ObservableCollection<NewsItem> NewsItems { get; } = new ObservableCollection<NewsItem>();

        public ANews(Config.Item newsConfig)
        {
            NewsConfig = newsConfig;
        }

        public void StartUpdate()
        {
            thread = new Thread(new ThreadStart(NewsThread));
            thread.Start();
        }

        public void StopUpdate()
        {
            thread.Abort();
        }

        private void NewsThread()
        {
            while(true)
            {
                Thread.Sleep(new TimeSpan(0, 30, 0)); //TODO: Thread Sleep Interval for news
                TriggerFeed();
            }
        }

        protected abstract void TriggerFeed();
    }
}
