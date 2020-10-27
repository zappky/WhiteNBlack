using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required]
        [Range(1, 100)]
        [DisplayName("Daily Cigarette Intake(In Sticks)")]
        public int cigaIntake { get; set; }
        [Required]
        [Range(1, 1000)]
        [DisplayName("Amount you spent on the average cigarette box")]
        public double cigaPrice { get; set; }
    }
}