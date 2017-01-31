using System;
using System.Collections.Generic;
using CrawlerLib.Model;
using HtmlAgilityPack;

namespace CrawlerLib.Helper
{
    public class Crawler
    {
        public static List<RecordValueViewModel> Crawl(MappingViewModel map)
        {
            List<RecordValueViewModel> records = new List<RecordValueViewModel>();
            HtmlDocument doc = new HtmlDocument();
            int counter = 0;
            var jsonResult = RequestHelper.RequestWithJsonContentType(map.Url);
            if (jsonResult != null)
            {
                //it's json type
                doc.LoadHtml(jsonResult.ToString());
                var container = doc.DocumentNode.SelectSingleNode(GetApproporiateStringForJson(map.Selector));
                if (map.Entity != null)
                {
                    while (true)
                    {
                        string xpath = GetApproporiateStringForJson(map.Entity.Selector);
                        var entities = container.SelectNodes(xpath);
                        if(entities==null || counter == entities.Count)
                            break;
                        var currentEntity = entities[counter++];
                        if (currentEntity == null)
                            break;
                        string reference = Guid.NewGuid().ToString();
                        foreach (var item in map.Entity.Properties)
                        {
                            string value = currentEntity.SelectSingleNode(GetApproporiateStringForJson(item.Selector)).InnerText;
                            records.Add(new RecordValueViewModel()
                            {
                                Value = value,
                                Reference = reference,
                                PropertyCode = item.Id
                            });
                        }

                    }                    
                    return records;
                }
                return null;
            }
            else
            {
                //it's html type, so jsonResult is null
                HtmlWeb web = new HtmlWeb();
                doc = web.Load(map.Url);
                var container = doc.DocumentNode.SelectSingleNode(map.Selector);
                if (map.Entity != null)
                {
                    while (true)
                    {
                        string xpath = map.Entity.Selector;
                        var entities = container.SelectNodes(xpath);
                        if (entities == null || counter == entities.Count)
                            break;
                        var currentEntity = entities[counter++];
                        if (currentEntity == null)
                            break;
                        string reference = Guid.NewGuid().ToString();
                        foreach (var item in map.Entity.Properties)
                        {
                            string value = currentEntity.SelectSingleNode(item.Selector).InnerText.Trim();
                            records.Add(new RecordValueViewModel()
                            {
                                Value = value,
                                Reference = reference,
                                PropertyCode = item.Id
                            });
                        }

                    }
                    return records;
                }
                return null;
            }
        }

        private static string GetApproporiateStringForJson(string input)
        {
            bool turn = true;
            string result = "";
            foreach (var item in input)
            {
                if (item != '\'') { result += item; }
                else { result += (turn) ? "'\\\"" : "\\\"'"; turn = !turn; }
            }
            return result;
        }
    }
}