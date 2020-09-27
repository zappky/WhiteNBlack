using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class Doctor : GeneralUser
    {
        public string workLocation { get; set; }
        public string description { get; set; }
        public int contactNo { get; set; }
        public string doctorID { get; set; }
        public bool adminVerify { get; set; }

        public Doctor()
        {

        }
        public Doctor(string userName, string name, string email, string password, DateTime dateOfBirth, string gender,
            string profilePicture, string workLocation, string description, int contactNo, string doctorID, bool adminVerify)
        {
            this.userName = userName;
            this.name = name;
            this.email = email;
            this.password = password;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.profilePicture = profilePicture;
            this.workLocation = workLocation;
            this.description = description;
            this.contactNo = contactNo;
            this.doctorID = doctorID;
            this.adminVerify = adminVerify;
        }
    }
}