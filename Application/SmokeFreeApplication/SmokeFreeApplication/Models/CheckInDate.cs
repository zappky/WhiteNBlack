using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class CheckInDate
    {
        [Key, Column(Order = 1)]
        public string checkInId { get; set; }
        public DateTime checkInDate { get; set; }
        public string userName { get; set; }
    }
}