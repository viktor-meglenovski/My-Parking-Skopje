namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class municipalityInParkings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parkings", "Municipality", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parkings", "Municipality");
        }
    }
}
