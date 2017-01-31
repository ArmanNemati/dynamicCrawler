using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CrawlerLibrary.Helper
{
    public class RequestHelper
    {
        public static string Request(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static JObject RequestWithJsonContentType(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    string text = sr.ReadToEnd();
                    return JObject.Parse(text);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}