using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    //Compound doctor & general user together. Makes it easier for crud on doctor.
    public class DoctorCompoundModel
    {
        [Required]
        public Doctor Doctors { get; set; }
        [Required]
        public GeneralUser GeneralUsers { get; set; }


    }
}