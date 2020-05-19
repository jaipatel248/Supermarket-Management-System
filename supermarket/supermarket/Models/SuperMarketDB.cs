namespace supermarket.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SuperMarketDB : DbContext
    {
        // Your context has been configured to use a 'SuperMarketDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'supermarket.Models.SuperMarketDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SuperMarketDB' 
        // connection string in the application configuration file.
        public SuperMarketDB()
            : base("name=SuperMarketDB")
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}