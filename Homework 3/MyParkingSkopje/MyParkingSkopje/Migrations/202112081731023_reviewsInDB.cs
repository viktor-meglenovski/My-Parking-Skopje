namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewsInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ParkingId = c.Int(nullable: false),
                        Stars = c.Int(nullable: false),
                        ReviewText = c.String(),
                    })
                .PrimaryKey(t => t.ReviewId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
