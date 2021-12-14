namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookmarks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookmarkId = c.Int(nullable: false, identity: true),
                        ParkingId = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.BookmarkId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bookmarks");
        }
    }
}
