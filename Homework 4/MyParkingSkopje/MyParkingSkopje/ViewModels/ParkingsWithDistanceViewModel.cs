using MyParkingSkopje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.ViewModels
{
    public class ParkingsWithDistanceViewModel
    {
        public Parking parking { get; set; }
        public Double distance { get; set; }
        public ParkingsWithDistanceViewModel() { }
        public ParkingsWithDistanceViewModel(Parking parking, Double distance)
        {
            this.parking = parking;
            this.distance = distance;
        }
    }
}