namespace WardobeLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedclasse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAccountDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsLogined = c.Boolean(nullable: false),
                        IdClothes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblClothesDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Color = c.String(nullable: false),
                        Description = c.String(),
                        IdAccount = c.Int(nullable: false),
                        IdCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategoriesDB", t => t.IdCategory, cascadeDelete: true)
                .ForeignKey("dbo.tblAccountDB", t => t.IdAccount, cascadeDelete: true)
                .Index(t => t.IdAccount)
                .Index(t => t.IdCategory);
            
            CreateTable(
                "dbo.tblCategoriesDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TemperatureMin = c.Double(nullable: false),
                        TemperatureMax = c.Double(nullable: false),
                        IdClothes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblClothesDB", "IdAccount", "dbo.tblAccountDB");
            DropForeignKey("dbo.tblClothesDB", "IdCategory", "dbo.tblCategoriesDB");
            DropIndex("dbo.tblClothesDB", new[] { "IdCategory" });
            DropIndex("dbo.tblClothesDB", new[] { "IdAccount" });
            DropTable("dbo.tblCategoriesDB");
            DropTable("dbo.tblClothesDB");
            DropTable("dbo.tblAccountDB");
        }
    }
}
