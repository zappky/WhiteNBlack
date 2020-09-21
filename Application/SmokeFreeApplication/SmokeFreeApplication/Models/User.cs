using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class User
    {
        public string userName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public string profilePicture { get; set; }
        public bool emailVerify { get; set; }

        public User()
        {
            
        }

        public User(string userName, string name, string email, string password, int age, string profilePicture, bool emailVerify)
        {
            this.userName = userName;
            this.name = name;
            this.email = email;
            this.password = password;
            this.age = age;
            this.profilePicture = profilePicture;
            this.emailVerify = emailVerify;
        }
    }
}