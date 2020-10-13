using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class Progress
    {
        [Key, Column(Order = 1)]
        public string userName { get; set; }
        public int totalCheckins { get; set; }
        public int cigaIntake { get; set; }
        public int cigaPrice { get; set; }
    }
}