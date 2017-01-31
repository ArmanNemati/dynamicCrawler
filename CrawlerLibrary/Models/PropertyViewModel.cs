using System.Collections.Generic;

namespace CrawlerLibrary.Models
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Selector { get; set; }
        public string Name { get; set; } 
        public int MappingCode { get; set; } 
        public MappingViewModel Mapping { get; set; }
    }
}
