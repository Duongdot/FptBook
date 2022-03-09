using System;
using System.Data.Entity;
using System.Linq;

namespace FptBookNew1.Models
{
    public class ModelDatabase : DbContext
    {
        public ModelDatabase()
            : base("name=ModelDatabase")
        {
        }


        public DbSet<account> accounts { get; set; }
        public DbSet<author> authors { get; set; }
        public DbSet<book> books { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<orderDetail> orderDetails { get; set; }
        public DbSet<order> orders { get; set; }

    }
}