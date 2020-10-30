using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmokeFreeApplication.Models
{
    public class InterestedParty 
    {
        [Key, Column(Order = 1)]
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DisplayName("Username")]
        public string userName { get; set; }

        [DisplayName("Are you a smoker or not")]
        [Required]
        public bool smokerOrNot { get; set; }
        [DisplayName("Bio")]
        public string bio { get; set; }

        public InterestedParty()
        {

        }
        public InterestedParty(string userName, bool smokerOrNot, string bio)
        {
            this.userName = userName;
            this.smokerOrNot = smokerOrNot;
            this.bio = bio;
        }
     
    }

   
}