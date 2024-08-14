namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "userName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "userName");
        }
    }
}
