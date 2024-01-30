using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Artwork_Sharing_Platform.Models
{
    public class YourDbContext : DbContext
    {
        public YourDbContext() : base("name=YourConnectionString")
        {
        }

        public DbSet<UserPlatform> UserPlatforms { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPlatform>().HasKey(u=> u.Id_User);

            
        }
    }
}