using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Article :Content
    {
        [Key, Column(Order = 1)]
        public int articleID { get; set; }
        public byte[] articlePicture { get; set; }
        public string articleStatus { get; set; }
    }



}