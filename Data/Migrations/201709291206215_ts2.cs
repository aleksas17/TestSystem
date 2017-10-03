namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ts2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
