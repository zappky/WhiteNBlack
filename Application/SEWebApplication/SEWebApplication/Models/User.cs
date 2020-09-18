using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEWebApplication.Models
{
    public class User
    {
        public string userName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public string profilePicture { get; set; }
        public Boolean verified { get; set; }
    }
}