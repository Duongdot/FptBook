namespace FptBookNew1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.accounts", "phone", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.accounts", "phone", c => c.String(nullable: false, maxLength: 13));
        }
    }
}
