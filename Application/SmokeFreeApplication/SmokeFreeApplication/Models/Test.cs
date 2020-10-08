using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace SmokeFreeApplication.Models
{
    public class Test
    {
        public int id { get; set; }
        public string name { get; set; }

        public int number { get; set; }
    }


    public class Test2
    {
        public int id { get; set; }
        public string name { get; set; }

        public int number { get; set; }
    }


    public class testDBContext : DbContext
    {
        //table1
        public DbSet<Test> testDatas { get; set; }
        //table2
        public DbSet<Test2> testDatas2 { get; set; }

    }


}

