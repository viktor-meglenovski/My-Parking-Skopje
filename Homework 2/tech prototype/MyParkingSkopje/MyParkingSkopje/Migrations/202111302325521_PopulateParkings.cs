namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class PopulateParkings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        ParkingId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Fee = c.Boolean(nullable: false),
                        Lattitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ParkingId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Parkings");
        }
    }
}
