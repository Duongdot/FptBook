using System;
using System.Data.Entity;
using System.Linq;

namespace FptBookNew1.Models
{
    public class ModelDatabase : DbContext
    {
        // Your context has been configured to use a 'ModelDatabase' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FptBookNew1.Models.ModelDatabase' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelDatabase' 
        // connection string in the application configuration file.
        public ModelDatabase()
            : base("name=ModelDatabase")
        {
        }


        public  DbSet<account> accounts { get; set; }
        public  DbSet<author> authors { get; set; }
        public  DbSet<book> books { get; set; }
        public  DbSet<category> categories { get; set; }
        public  DbSet<feedback> feedbacks { get; set; }
        public  DbSet<orderDetail> orderDetails { get; set; }
        public  DbSet<order> orders { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}