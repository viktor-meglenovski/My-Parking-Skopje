namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateToReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "datetime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "datetime");
        }
    }
}
