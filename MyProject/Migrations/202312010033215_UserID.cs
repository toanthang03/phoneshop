namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "UserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "UserID");
        }
    }
}
