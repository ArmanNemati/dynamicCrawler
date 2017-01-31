namespace DynamicCrawler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        Key = c.String(),
                        FirstSelector = c.String(),
                        SecondSelector = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Selector = c.String(),
                        Name = c.String(),
                        MappingCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mapping", t => t.MappingCode, cascadeDelete: true)
                .Index(t => t.MappingCode);
            
            CreateTable(
                "dbo.Url",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        Link = c.String(),
                        MappingCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mapping", t => t.MappingCode, cascadeDelete: true)
                .Index(t => t.MappingCode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Url", "MappingCode", "dbo.Mapping");
            DropForeignKey("dbo.Property", "MappingCode", "dbo.Mapping");
            DropIndex("dbo.Url", new[] { "MappingCode" });
            DropIndex("dbo.Property", new[] { "MappingCode" });
            DropTable("dbo.Url");
            DropTable("dbo.Property");
            DropTable("dbo.Mapping");
        }
    }
}
