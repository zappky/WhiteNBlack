using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class Admin
    {
        [DisplayName("Username")]
        public string id { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }

        public Admin()
        {
        
        }
    }
}