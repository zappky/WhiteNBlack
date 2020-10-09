﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Comment
    {
        [Key, Column(Order = 1)]
        public int commentID { get; set; }
        public string parentType { get; set; }
        public int parentID { get; set; }
        
        [StringLength(2000)]
        public string body { get; set; }
        public DateTime postDate { get; set; }
        public string userName { get; set; }

    }
}