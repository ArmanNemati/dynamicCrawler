using System;
using DynamicCrawler.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CrawlerLibrary.Helper;
using CrawlerLibrary.Models;
using DynamicCrawler.Helper;
using DynamicCrawler.Models.ViewModel;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace DynamicCrawler.Controllers
{
    public class HomeController : Controller
    {
        readonly DynamicCrawlerDBContext _db;
        public HomeController()
        {
            _db = new DynamicCrawlerDBContext();
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TransferCrawlRequest()
        {
            var mapping = new MappingRepository().Search(StaticString.TransferCrawlingKey);
            if (mapping != null)
            {
                List<RecordValueViewModel> result = Crawler.Crawl(Factory.Convert(mapping));
                var transferList = ClassMapper.Map<Transfer>(result).OrderBy(t=>t.Team).ThenBy(t=>t.TransferType);
                return View(transferList);
            }
            return null;
        }

        public ActionResult MatchCrawlRequest()
        {
            var mapping = new MappingRepository().Search(StaticString.MatchCrawlingKey);
            if (mapping != null)
            {
                List<RecordValueViewModel> result = Crawler.Crawl(Factory.Convert(mapping));
                var matchList = ClassMapper.Map<Match>(result).OrderBy(m => m.League);
                return View(matchList);
            }
            return null;
        }

        public ActionResult V3Request() 
        {
            var mapping = new MappingRepository().Search("v3");
            if (mapping != null)
            {
                List<RecordValueViewModel> result = Crawler.Crawl(Factory.Convert(mapping));
                var matchList = ClassMapper.Map<News>(result);
                return View(matchList);
            }
            return null;
        }

        public ActionResult CreateTransferUrls()
        {
            string url = "http://uk.soccerway.com/players/transfers/?ICID=TN_04";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);

            //first get league
            List<string> leagueValues = new List<string>();
            List<string> leagueUrls = new List<string>();
            int counter = 1;
            while (true)
            {
                string dropXpath = string.Format("//*[@id='competition-select']/option[{0}]", counter++);
                var drop = document.DocumentNode.SelectSingleNode(dropXpath);
                if (drop == null)
                    break;
                leagueValues.Add(drop.GetAttributeValue("value", ""));
                leagueUrls.Add(
                    string.Format(
                        "http://uk.soccerway.com/a/block_players_transfers?block_id=page_players_1_block_players_transfers_4&callback_params=%7B%22year%22%3A%222017%22%2C%22season_id%22%3A%22{0}%22%7D&action=showTransfers&params=%7B%22season_id%22%3A%22{1}%22%7D",
                        (leagueValues.Count >= 1) ? leagueValues[leagueValues.Count - 1] : "11360",
                        drop.GetAttributeValue("value", "")));
            }
            List<Url> lstUrls=new List<Url>();
            var mapping = new MappingRepository().Search(StaticString.TransferCrawlingKey);
            foreach (string leagueUrl in leagueUrls)
            {
                lstUrls.Add(new Url()
                {
                    Link = leagueUrl,
                    MappingCode = mapping.Id,
                });
            }
            _db.Urls.AddRange(lstUrls);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
