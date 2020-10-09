using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmokeFreeApplication.Models
{
    public class SmokeFreeDBContext : DbContext
    {
        public DbSet<GeneralUser> GeneralUser { get; set; }
        public DbSet<InterestedParty> InterestedParty { get; set; }
        public DbSet<Doctor> Doctor { get; set; }

    }
}