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
        public DbSet<Article> Article { get; set; }
        public DbSet<Story> Story { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<ArticlesTag> ArticlesTag { get; set; }
        public DbSet<StoriesTag> StoriesTag { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<BroadcastMessage> BroadCastMessage { get; set; }

    }
}