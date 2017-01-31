using System.Collections.Generic;

namespace DynamicCrawler.Models 
{
    public class Mapping
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string Key { get; set; } 
        public string FirstSelector { get; set; }
        public string SecondSelector { get; set; }
        public List<Property> Properties { get; set; }
        public List<Url> Urls { get; set; } 
    }
}
