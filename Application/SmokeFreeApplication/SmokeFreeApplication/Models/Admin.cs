using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class Admin :UserEntry
    {
        public string id { get; set; }

        public Admin()
        {
        
        }

        public bool BanContent(Content aContent)
        {
            return aContent.DeleteContent();
        }
    }
}