using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingMicroservice.Models
{
    public class Parking
    {
        public int ParkingId { get; set; }
        public string Name { get; set; }
        public Boolean Fee { get; set; }
        public Double Lattitude { get; set; }
        public Double Longitude { get; set; }
        public string Municipality { get; set; }
        public Parking(string Name, Boolean Fee, Double Lattitude, Double Longitude, string Municipality)
        {
            this.Name = Name;
            this.Fee = Fee;
            this.Lattitude = Lattitude;
            this.Longitude = Longitude;
            this.Municipality = Municipality;
        }
        public Parking() { }
    }
}