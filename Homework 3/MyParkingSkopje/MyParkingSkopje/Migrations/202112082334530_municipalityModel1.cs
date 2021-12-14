namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class municipalityModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Municipalities",
                c => new
                    {
                        MunicipalityId = c.Int(nullable: false, identity: true),
                        MunicipalityName = c.String(),
                    })
                .PrimaryKey(t => t.MunicipalityId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Municipalities");
        }
    }
}
