using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DynamicCrawler.Models
{
    public class DynamicCrawlerDBContext : DbContext
    {
        public DynamicCrawlerDBContext() : base("CrawlerConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<RecordValue>()
            //.HasRequired<Record>(s => s.Record)
            //.WithMany()
            //.WillCascadeOnDelete(false);
        }
        public DbSet<Mapping> Mappings { get; set; }
        public DbSet<Url> Urls { get; set; } 
        public DbSet<Property> Properties { get; set; }
    }
}

