using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DynamicCrawler.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string Link { get; set; }

        [ForeignKey("Mapping")]
        public int MappingCode { get; set; }
        public Mapping Mapping { get; set; } 
    }
}