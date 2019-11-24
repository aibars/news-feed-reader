using NewsFeedReader.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace NewsFeedReader.Logic
{
    public static class FeedParser
    {
        public static List<Feed> Parse(string url)
        {
            return ParseRss(url);
        }

        public static List<Feed> ParseRss(string url)
        {
            var entries = new List<Feed>();
            try
            {
                XDocument doc = XDocument.Load(url);

                if (doc.Root != null)
                {
                    var channels = doc.Root.Descendants().FirstOrDefault(i => i.Name.LocalName.Equals("channel"));
                    if (channels != null)
                    {
                        var items = channels.Elements().Where(x => x.Name.LocalName.Equals("item")).Select(y =>
                        {
                            var feed = new Feed();

                            var descriptionNode = y.Elements().FirstOrDefault(i => i.Name.LocalName == "description");
                            if (descriptionNode != null)
                            {
                                feed.Content = descriptionNode.Value;
                            }

                            var linkNode = y.Elements().FirstOrDefault(i => i.Name.LocalName == "link");
                            if (linkNode != null)
                            {
                                feed.Link = linkNode.Value;
                            }

                            var pubDateNode = y.Elements().FirstOrDefault(i => i.Name.LocalName == "pubDate");
                            if (pubDateNode != null)
                            {
                                feed.PublishDate = ParseDate(pubDateNode.Value);
                            }

                            var titleNode = y.Elements().FirstOrDefault(i => i.Name.LocalName == "title");
                            if (titleNode != null)
                            {
                                feed.Title = titleNode.Value;
                            }

                            return feed;
                        });

                        entries = items.ToList();
                    }
                }
            }
            catch
            {
                Trace.TraceError("Error parsing the RSS Feed in the URL: " + url + ". Returning empty list.");
            }

            return entries;
        }

        private static DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }
    }
}
