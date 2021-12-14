namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class populateParkingsMunicipalities : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Parkings");
            using (var reader = new StreamReader(@"C:\Users\Viktor Meglenovski\Documents\GitHub\My-Parking-Skopje\Homework 3\MyParkingSkopje\MyParkingSkopje\Content\Data\outputDataNew.txt", System.Text.Encoding.UTF8))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    Boolean fee = values[2] == "yes" ? true : false;
                    Sql("INSERT INTO Parkings (Name, Fee, Lattitude, Longitude, Municipality) VALUES(N'" + values[1] + "','" + fee + "'," + Double.Parse(values[3]) + "," + Double.Parse(values[4]) + ",N'"+values[5]+"')");
                }
            }
        }
        
        public override void Down()
        {
        }
    }
}
