
using System.ComponentModel;
using System.Net.Http.Headers;
// using api.EntityFramework;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
public class AppDBContext: DbContext  { 
    public AppDBContext(DbContextOptions options) : base(options){}
    
    // DbSet properties for our entities
    // ! it cause an error when I create appDbContext for customer order.
    // ! The name what causing the error (CustomerOrders)
    // public IEnumerable<object> CustomerOrders { get; internal set; }

     public DbSet<User> Users { get; set; }

    // public DbSet<Product>Products {get; set; }
    public DbSet<Category> Categories {get; set; }
    // public DbSet<OrderDetail> Orders {get; set; }
     public DbSet<CustomerOrder> CustomerOrders {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<CustomerOrder>().HasKey(o => o.OrderId);
        
    }

}
