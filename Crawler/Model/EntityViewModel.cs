using System.Collections.Generic;
using Crawler.Model;

namespace CrawlerLib.Model
{
    public class EntityViewModel
    {
        public int MappingCode { get; set; }
        public MappingViewModel Mapping { get; set; }
        public string Name { get; set; }
        public string Selector { get; set; }
        public List<PropertyViewModel> Properties { get; set; }
    }
}