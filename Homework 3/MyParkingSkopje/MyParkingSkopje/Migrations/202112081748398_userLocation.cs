namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLocations",
                c => new
                    {
                        UserLocationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Lattitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UserLocationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserLocations");
        }
    }
}
