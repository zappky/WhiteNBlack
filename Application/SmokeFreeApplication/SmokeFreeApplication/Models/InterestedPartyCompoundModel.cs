using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class InterestedPartyCompoundModel
    {
        [Required]
        public InterestedParty InterestedParties { get; set; }
        [Required]
        public GeneralUser GeneralUsers { get; set; }
    }
}