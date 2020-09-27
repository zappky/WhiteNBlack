using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class GeneralUser
    {
        public string userName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string gender { get; set; }
        public string profilePicture { get; set; }

        public GeneralUser()
        {
            
        }

        public GeneralUser(string userName, string name, string email, string password, DateTime dateOfBirth, string gender, string profilePicture)
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