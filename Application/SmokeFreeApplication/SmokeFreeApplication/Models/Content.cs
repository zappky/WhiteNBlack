using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Content
    {
        public string userName { get; set; }

        //List<string> Pictures { get; set; }

        public string title { get; set; }
        [AllowHtml]
        public string body { get; set; }
        public DateTime postDate { get; set; }


    }

}