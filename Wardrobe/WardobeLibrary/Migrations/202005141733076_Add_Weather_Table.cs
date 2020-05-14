namespace WardobeLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Weather_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblWeatherDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Region = c.String(),
                        City = c.String(),
                        TimeOfInfo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblWeatherDB");
        }
    }
}
