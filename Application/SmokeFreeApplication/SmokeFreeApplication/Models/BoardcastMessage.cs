using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class BoardcastMessage
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string ownerName { get; set; }
        public DateTime postTime { get; set; }
        public DateTime expiresTime { get; set; }
        public bool neverShowAgain { get; set; }
    }
}