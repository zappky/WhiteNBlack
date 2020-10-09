using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmokeFreeApplication.Models
{
    public class Tag
    {
        [Key, Column(Order = 1)]
        public int tagID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string tagName { get; set; }
    }
}