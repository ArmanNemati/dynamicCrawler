using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using CrawlerLibrary.Models;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace CrawlerLibrary.Helper
{
    public class Crawler
    {
        public static List<RecordValueViewModel> Crawl(MappingViewModel map)
        {
            List<RecordValueViewModel> records = new List<RecordValueViewModel>();
            if (map.Urls == null || map.Urls.Count == 0) return records;
            foreach (string url in map.Urls)
            {
                var jsonResult = RequestHelper.RequestWithJsonContentType(url);
                var lstResult = jsonResult != null ? GetReordsFromJson(jsonResult, map) : GetReordsFromHtml(url, map);
                records.AddRange(lstResult ?? new List<RecordValueViewModel>());
            }
            return records;
        }

        private static List<RecordValueViewModel> GetReordsFromJson(JObject jsonResult, MappingViewModel map)
        {
            List<RecordValueViewModel> records = new List<RecordValueViewModel>();
            HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(jsonResult.ToString());
            var container = doc.DocumentNode.SelectSingleNode(GetApproporiateStringForJson(map.FirstSelector));
            if (container == null)
                return null;

            string xpath = GetApproporiateStringForJson(map.SecondSelector);
            var entities = container.SelectNodes(xpath);
            if (entities == null)
                return null;
            int counter = 0;
            while (true)
            {
                if (counter == entities.Count)
                    break;
                var currentEntity = entities[counter++];
                if (currentEntity == null)
                    break;
                string reference = Guid.NewGuid().ToString();
                foreach (var item in map.Properties)
                {
                    string value = currentEntity.SelectSingleNode(GetApproporiateStringForJson(item.Selector)).InnerText;
                    records.Add(new RecordValueViewModel()
                    {
                        Value = value,
                        Reference = reference,
                        PropertyCode = item.Id,
                        Property = item
                    });
                }

            }
            return records;
        }

        private static List<RecordValueViewModel> GetReordsFromHtml(string url, MappingViewModel map)
        {
            List<RecordValueViewModel> records = new List<RecordValueViewModel>();
            //HtmlWeb web = new HtmlWeb();
            //var doc = web.Load(url);
            var doc = LoadHtmlWithAjaxContent(url); //it's useful when we have ajax content in page
            var container = doc.DocumentNode.SelectSingleNode(map.FirstSelector);
            if (container == null)
                return null;

            string xpath = map.SecondSelector;
            var entities = container.SelectNodes(xpath);
            if (entities == null)
                return null;
            int counter = 0;
            while (true)
            {
                if (counter == entities.Count)
                    break;
                var currentEntity = entities[counter++];
                if (currentEntity == null)
                    break;
                string reference = Guid.NewGuid().ToString();
                foreach (var item in map.Properties)
                {
                    string value = currentEntity.SelectSingleNode(item.Selector).InnerText.Trim();
                    records.Add(new RecordValueViewModel()
                    {
                        Value = value,
                        Reference = reference,
                        PropertyCode = item.Id,
                        Property = item
                    });
                }

            }
            return records;
        }

        static HtmlDocument LoadHtmlWithAjaxContent(string url)
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                webReq.CookieContainer = new CookieContainer();
                webReq.Method = "GET";
                using (WebResponse response = webReq.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        var res = reader.ReadToEnd();
                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(res);
                        return htmlDocument;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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