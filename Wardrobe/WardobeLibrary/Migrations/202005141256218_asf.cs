namespace WardobeLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asf : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblAccountDB", "IdClothes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblAccountDB", "IdClothes", c => c.Int(nullable: false));
        }
    }
}
