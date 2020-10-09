using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Article :Content
    {
        public int ArticleID { get; set; }
        public string ArticlePicture { get; set; }
        public string ArticleStatus { get; set; }
    }



}