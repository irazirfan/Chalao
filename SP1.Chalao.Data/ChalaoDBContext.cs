using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;

namespace SP1.Chalao.Data
{
    public class ChalaoDBContext : DbContext
    {
        public ChalaoDBContext() : base("ChalaoConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasOptional(a => a.Rider)
                .WithRequired(ab => ab.Users);
        }

        public DbSet<Users> Users { get; set; }
         public DbSet<Admins> Admins { get; set; }
         public DbSet<Employees> Employees { get; set; }
         public DbSet<Riders> Riders { get; set; }
         
    }
}
