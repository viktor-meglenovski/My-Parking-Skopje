using MyParkingSkopje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.ViewModels
{
    public class SearchParkingsIndexViewModel
    {
        public List<Municipality> municipalities { get; set; }
        public List<ParkingDetailsWithReviews> topFiveParkings { get; set; }
        public SearchParkingsIndexViewModel() 
        {
            this.municipalities = new List<Municipality>();
            this.topFiveParkings = new List<ParkingDetailsWithReviews>();
        }
        public SearchParkingsIndexViewModel(List<Municipality> municipalities,List<ParkingDetailsWithReviews> topFiveParkings) 
        {
            this.municipalities = new List<Municipality>();
            this.topFiveParkings = new List<ParkingDetailsWithReviews>();
            this.municipalities = municipalities;
            this.topFiveParkings = topFiveParkings;
        }
    }
}