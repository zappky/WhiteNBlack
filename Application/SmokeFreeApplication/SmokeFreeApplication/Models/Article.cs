using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Article :Content
    {
        public int articleID { get; set; }
        public string articlePicture { get; set; }
        public string articleStatus { get; set; }
    }



}