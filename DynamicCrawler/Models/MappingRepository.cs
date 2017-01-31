using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DynamicCrawler.Helper;

namespace DynamicCrawler.Models
{
    public class MappingRepository
    {
        public Mapping Search(string key)
        {
            return new DynamicCrawlerDBContext().Mappings.Include(m=>m.Urls).Include(m => m.Properties).SingleOrDefault(m => m.Key == key);
        }
    }
}