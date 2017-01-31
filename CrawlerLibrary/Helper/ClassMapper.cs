using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CrawlerLibrary.Models;

namespace CrawlerLibrary.Helper
{
    public class ClassMapper
    {
        public static List<T> Map<T>(List<RecordValueViewModel> rocords) where T : new()
        {
            List<T> result = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            var referenceList = rocords.DistinctBy(r => r.Reference).Select(r => r.Reference).ToList();
            foreach (var reference in referenceList)
            {
                T obj = new T();
                List<RecordValueViewModel> itemProperties = rocords.Where(r => r.Reference == reference).ToList();
                foreach (PropertyInfo property in properties)
                {
                    var prop = itemProperties.SingleOrDefault(r => r.Property.Name == property.Name);
                    if (prop != null)
                        property.SetValue(obj, prop.Value);
                }
                result.Add(obj);
            }
            return result;
        }
    }
}