using MyParkingSkopje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.ViewModels
{
    public class ReviewDetails
    {
        public Review review { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public ReviewDetails() { }
        public ReviewDetails(Review review, string name, string surname)
        {
            this.review = review;
            this.name = name;
            this.surname = surname;
        }
    }
}