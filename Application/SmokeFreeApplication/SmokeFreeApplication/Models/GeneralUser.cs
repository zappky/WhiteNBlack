using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class GeneralUser
    {
        [Key, Column(Order = 1)]
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DisplayName("Username")]
        public string userName { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Name")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The password cannot be shorter than 8 characters")]
        [DisplayName("Password")]
        public string password { get; set; }
        [NotMapped]
        [Required]
        [DisplayName("Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("password")]
        public string confirmPassword { get; set; }
        [Required]
        [DisplayName("Date of Birth")]
        [Range(typeof(DateTime), "1/1/1900", "31/12/2004",
                ErrorMessage = "You must be at least 16 years old to register.")]
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }
        [Required]
        [DisplayName("Gender")]
        public string gender { get; set; }
        [DisplayName("Profile Picture")]
        public byte[] profilePicture { get; set; }

        public GeneralUser()
        {
            
        }

        public GeneralUser(string userName, string name, string email, string password, DateTime dateOfBirth, string gender, byte[] profilePicture)
        {
            this.userName = userName;
            this.name = name;
            this.email = email;
            this.password = password;
            this.dateOfBirth = dateOfBirth;
            this.profilePicture = profilePicture;
            this.gender = gender;
        }

        
    }

}