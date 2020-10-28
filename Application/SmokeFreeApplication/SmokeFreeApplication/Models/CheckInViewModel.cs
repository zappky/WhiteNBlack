using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class CheckInViewModel
    {
        //public string userName { get; set; }
        public string startStr { get; set; }
        public string endStr { get; set; }
        public bool allDay { get; set; }
        public string display { get; set; }
        public string color { get; set; }
    }
}