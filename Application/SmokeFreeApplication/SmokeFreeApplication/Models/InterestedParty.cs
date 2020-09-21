using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class InterestedParty : User
    {
        public bool smokerOrNot { get; set; }
        public string bio { get; set; }

        public InterestedParty()
        {

        }

        public InterestedParty(string userName, string name, string email, int age, string profilePicture, bool emailVerify, bool smokerOrNot, string bio)
        {
            this.userName = userName;
            this.name = name;
            this.email = email;
            this.age = age;
            this.profilePicture = profilePicture;
            this.emailVerify = emailVerify;
            this.smokerOrNot = smokerOrNot;
            this.bio = bio;
        }
    }
}