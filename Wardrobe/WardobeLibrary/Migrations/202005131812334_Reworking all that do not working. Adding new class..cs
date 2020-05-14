namespace WardobeLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkingallthatdonotworkingAddingnewclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblClothesDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Color = c.String(),
                        Description = c.String(),
                        IdAccount = c.Int(nullable: false),
                        IdCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblAccountDB", t => t.IdAccount, cascadeDelete: true)
                .ForeignKey("dbo.tblCategoriesDB", t => t.IdCategory, cascadeDelete: true)
                .Index(t => t.IdAccount)
                .Index(t => t.IdCategory);
            
            CreateTable(
                "dbo.tblAccountDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Token = c.String(),
                        IdClothes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblCategoriesDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TemperatureMin = c.Double(nullable: false),
                        TemperatureMax = c.Double(nullable: false),
                        IdClothes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblClothesDB", "IdCategory", "dbo.tblCategoriesDB");
            DropForeignKey("dbo.tblClothesDB", "IdAccount", "dbo.tblAccountDB");
            DropIndex("dbo.tblClothesDB", new[] { "IdCategory" });
            DropIndex("dbo.tblClothesDB", new[] { "IdAccount" });
            DropTable("dbo.tblCategoriesDB");
            DropTable("dbo.tblAccountDB");
            DropTable("dbo.tblClothesDB");
        }
    }
}
