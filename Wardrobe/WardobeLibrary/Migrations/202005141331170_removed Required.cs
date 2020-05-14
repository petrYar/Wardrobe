namespace WardobeLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblAccountDB", "Token", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblAccountDB", "Token", c => c.String(nullable: false));
        }
    }
}
