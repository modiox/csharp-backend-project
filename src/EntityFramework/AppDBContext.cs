
using System.ComponentModel;
using System.Net.Http.Headers;
using api.EntityFramework;
using Microsoft.EntityFrameworkCore;
public class AppDBContext: DbContext  { 
    public AppDBContext(DbContextOptions options) : base(options){}
    
    // DbSet properties for our entities
     public DbSet<User> Users { get; set; }
    // public DbSet<Product>Products {get; set; }
    public DbSet<Category> Categories {get; set; }
    // public DbSet<OrderDetail> Orders {get; set; }
     public DbSet<CustomerOrder> CustomerOrders {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }

}
