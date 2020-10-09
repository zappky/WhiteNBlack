using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


//Feel free to change it, just placeholder for now - kee yang
namespace SmokeFreeApplication.Models
{
    public class Comment
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; }
        public string ParentType { get; set; }
        public int ParentId { get; set; }
        
        [StringLength(2000)]
        public string Body { get; set; }
        public DateTime PostDate { get; set; }
        public string UserName { get; set; }

    }
}