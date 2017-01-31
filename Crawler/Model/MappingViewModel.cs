namespace CrawlerLib.Model 
{
    public class MappingViewModel
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string Url { get; set; }
        public string Selector { get; set; }
        public EntityViewModel Entity { get; set; }
    }
}
