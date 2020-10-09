﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmokeFreeApplication.Models
{
    public class StoriesTag
    {
        [Key, Column(Order = 1)]
        public int tagID { get; set; }
        public int storyID { get; set; }
    }
}