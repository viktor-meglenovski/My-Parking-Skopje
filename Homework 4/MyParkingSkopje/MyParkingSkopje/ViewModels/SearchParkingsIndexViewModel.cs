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
        public List<ParkingDetails> topFiveParkings { get; set; }
        public SearchParkingsIndexViewModel() 
        {
            this.municipalities = new List<Municipality>();
            this.topFiveParkings = new List<ParkingDetails>();
        }
        public SearchParkingsIndexViewModel(List<Municipality> municipalities,List<ParkingDetails> topFiveParkings) 
        {
            this.municipalities = new List<Municipality>();
            this.topFiveParkings = new List<ParkingDetails>();
            this.municipalities = municipalities;
            this.topFiveParkings = topFiveParkings;
        }
    }
}