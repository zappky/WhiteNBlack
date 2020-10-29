using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class ProgressViewModel
    {
        public string userName { get; set; }
        public int streak { get; set; }
        public int totalCheck { get; set; }
        public int cigaSaved { get; set; }
        public double cashSaved { get; set; }
    }
}