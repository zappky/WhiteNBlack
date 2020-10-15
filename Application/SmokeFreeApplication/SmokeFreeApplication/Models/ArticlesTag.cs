using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmokeFreeApplication.Models
{
    public class ArticlesTag
    {
        [Key, Column(Order = 1)]
        public int tagID { get; set; }
        [Key, Column(Order = 2)]
        public int articleID { get; set; }
    }
}