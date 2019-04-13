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
         public DbSet<Users> Users { get; set; }
         public DbSet<Admins> Admins { get; set; }
         public DbSet<Employees> Employeeses { get; set; }
         
    }
}
