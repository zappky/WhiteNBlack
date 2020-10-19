using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class Doctor
    {
        [Key, Column(Order = 1)]
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DisplayName("Username")]
        public string userName { get; set; }
        [Required]
        [DisplayName("Name of practice place")]
        public string workLocation { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public int contactNo { get; set; }

        [Required]
        [DisplayName("Doctor Identification Number")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Doctor identification is at least 5 characters long")]
        public string doctorID { get; set; }
     
        [Required]
        public bool adminVerify { get; set; }

        public Doctor()
        {

        }

        
       
    }
}