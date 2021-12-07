using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.Models
{
    public class Parking
    {
        [Key]
        public int ParkingId { get; set; }
        public string Name { get; set; }
        public Boolean Fee { get; set; }
        public Double Lattitude { get; set; }
        public Double Longitude { get; set; }
        public Parking(string Name, Boolean Fee, Double Lattitude, Double Longitude)
        {
            this.Name = Name;
            this.Fee = Fee;
            this.Lattitude = Lattitude;
            this.Longitude = Longitude;
        }
        public Parking() { }
    }
}