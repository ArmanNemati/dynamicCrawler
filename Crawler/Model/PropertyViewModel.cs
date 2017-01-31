using System.Collections.Generic;
using CrawlerLib.Model;

namespace Crawler.Model
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Selector { get; set; }
        public string Name { get; set; } 
        public int EntityCode { get; set; } 
        public EntityViewModel Entity { get; set; }
        public List<RecordValueViewModel> RecordValues { get; set; } 
    }
}
