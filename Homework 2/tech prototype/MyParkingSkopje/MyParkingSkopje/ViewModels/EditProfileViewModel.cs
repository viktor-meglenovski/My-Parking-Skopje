using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.ViewModels
{
    public class EditProfileViewModel
    {
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ProfilePictureUrl { get; set; }
        public EditProfileViewModel() { }
        public EditProfileViewModel(string userId, string firstName, string lastName, string profilePictureUrl)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}