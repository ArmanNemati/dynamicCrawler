using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrawlerLibrary.Models;
using DynamicCrawler.Models;

namespace DynamicCrawler.Helper
{
    public class Factory
    {
        public static MappingViewModel Convert(Mapping mapping)
        {
            return new MappingViewModel()
            {
                Id = mapping.Id,
                Comments = mapping.Comments,
                FirstSelector = mapping.FirstSelector,
                SecondSelector = mapping.SecondSelector,
                Key = mapping.Key,
                Properties = Convert(mapping.Properties),
                Urls = (mapping.Urls != null) ? mapping.Urls.Select(u => u.Link).ToList() : new List<string>()
            };
        }

        private static List<PropertyViewModel> Convert(List<Property> properties)
        {
            List<PropertyViewModel> lstProperties=new List<PropertyViewModel>();
            foreach (Property property in properties)
            {
                lstProperties.Add(Convert(property));
            }
            return lstProperties;
        }

        public static PropertyViewModel Convert(Property property)
        {
            return new PropertyViewModel()
            {
                Selector = property.Selector,
                Name = property.Name,
                Id = property.Id,
                //Mapping = Convert(property.Mapping),
                MappingCode = property.MappingCode,
            };
        }
    }
}