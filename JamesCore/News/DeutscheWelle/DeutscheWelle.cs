using System;
using Fizzler.Systems.HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using HtmlAgilityPack;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace JamesCore.News.DeutscheWelle
{
    class DeutscheWelle : ANews
    {
        public DeutscheWelle(Config.Item newsConfig) : base(newsConfig)
        {
            TriggerFeed();
        }

        protected override async void TriggerFeed()
        {
            SyndicationFeed rssData;
            string xml = await HttpClient.GetStringAsync(NewsConfig.url).ConfigureAwait(true);

            using (TextReader reader = new StringReader(xml))
            {
                XmlReader xmlReader = XmlReader.Create(reader);
                rssData = SyndicationFeed.Load(xmlReader);
                xmlReader.Dispose();
            }

            FillNewsItems(rssData);
        }

        private async void FillNewsItems(SyndicationFeed rssData)
        {
            NewsItems.Clear();

            foreach (SyndicationItem rssItem in rssData.Items)
            {
                if (NewsItems.Count <= 1)
                {
                    NewsItem newsItem = new NewsItem();
                    newsItem.title = rssItem.Title.Text;
                    newsItem.content = "";

                    string htmlString = await HttpClient.GetStringAsync(rssItem.Links.First().Uri.AbsoluteUri).ConfigureAwait(true);
                    HtmlDocument newsHtml = new HtmlDocument();

                    using (TextReader reader = new StringReader(htmlString))
                    {
                        newsHtml.Load(reader);
                    }

                    HtmlNode longtext = newsHtml.DocumentNode.QuerySelectorAll(NewsConfig.parentContainerClass).ToList().First();

                    foreach (HtmlNode contentParts in longtext.ChildNodes)
                    {
                        if (contentParts.Name == NewsConfig.textContainerTag)
                        {
                            newsItem.content += contentParts.InnerText + " ";
                        }
                    }

                    NewsItems.Add(newsItem);
                }
            }
        }
    }
}
