using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicCrawler.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Selector { get; set; }
        public string Name { get; set; } 


        [ForeignKey("Mapping")]
        public int MappingCode { get; set; } 
        public Mapping Mapping { get; set; }
    }
}
