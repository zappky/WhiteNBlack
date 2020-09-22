using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class Doctor : User
    {
        private string workLocation { get; set; }
        private string description { get; set; }
        private int contactNo { get; set; }
        private bool adminVerify { get; set; }

        public Doctor(string userName, string name, string email, string password, int age, 
            string profilePicture, bool emailVerify, string workLocation, string description, int contactNo, bool adminVerify)
        {
            this.userName = userName;
            this.name = name;
            this.email = email;
            this.password = password;
            this.age = age;
            this.profilePicture = profilePicture;
            this.emailVerify = emailVerify;
            this.workLocation = workLocation;
            this.description = description;
            this.contactNo = contactNo;
            this.adminVerify = adminVerify;
        }
    }
}