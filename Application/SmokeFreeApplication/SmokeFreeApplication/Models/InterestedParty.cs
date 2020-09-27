using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class InterestedParty : GeneralUser
    {
        public bool smokerOrNot { get; set; }
        public string bio { get; set; }

        public InterestedParty()
        {

        }

        public InterestedParty(string userName, string name, string email, string password, DateTime dateOfBirth,string gender, string profilePicture, bool smokerOrNot, string bio)
        {
            this.userName = userName;
            this.name = name;
            this.email = email;
            this.password = password;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.profilePicture = profilePicture;
            this.smokerOrNot = smokerOrNot;
            this.bio = bio;
        }
    }
}