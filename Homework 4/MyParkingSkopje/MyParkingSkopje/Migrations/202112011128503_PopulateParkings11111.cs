namespace MyParkingSkopje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class PopulateParkings11111 : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Parkings");
            using (var reader = new StreamReader(@"C:\Users\Viktor Meglenovski\Desktop\MyParkingSkopje\MyParkingSkopje\Content\Data\outputData.csv", System.Text.Encoding.UTF8))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    Boolean fee = values[2] == "yes" ? true : false;
                    Sql("INSERT INTO Parkings (Name, Fee, Lattitude, Longitude) VALUES(N'" + values[1] + "','" + fee + "'," + Double.Parse(values[3]) + "," + values[4] + ")");
                }
            }
        }
        
        public override void Down()
        {
        }
    }
}
