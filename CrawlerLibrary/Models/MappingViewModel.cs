using System.Collections.Generic;

namespace CrawlerLibrary.Models 
{
    public class MappingViewModel
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string Key { get; set; }
        public string FirstSelector { get; set; }
        public string SecondSelector { get; set; }
        public List<string> Urls { get; set; }
        public List<PropertyViewModel> Properties { get; set; } 
    }
}
