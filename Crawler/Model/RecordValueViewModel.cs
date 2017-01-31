using Crawler.Model;

namespace CrawlerLib.Model
{
    public class RecordValueViewModel
    {
        public string Reference { get; set; }
        public int PropertyCode { get; set; }
        public PropertyViewModel Property { get; set; }
        public string Value { get; set; }
    }
}
