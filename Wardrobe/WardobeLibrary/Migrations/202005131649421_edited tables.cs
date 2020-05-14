namespace WardobeLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedtables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblAccountDB", "Token", c => c.String(nullable: false));
            DropColumn("dbo.tblAccountDB", "IsLogined");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblAccountDB", "IsLogined", c => c.Boolean(nullable: false));
            DropColumn("dbo.tblAccountDB", "Token");
        }
    }
}
