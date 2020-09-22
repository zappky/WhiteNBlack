using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class User
    {
        protected string userName { get; set; }
        protected string name { get; set; }
        protected string email { get; set; }
        protected string password { get; set; }
        protected int age { get; set; }
        protected string profilePicture { get; set; }
        protected bool emailVerify { get; set; }

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