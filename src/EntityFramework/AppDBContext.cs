
using System.ComponentModel;
using System.Net.Http.Headers;
// using api.EntityFramework;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions options) : base(options) { }

    // DbSet properties for our entities
    // ! it cause an error when I create appDbContext for customer order.
    // ! The name what causing the error (CustomerOrders)
    // public IEnumerable<object> CustomerOrders { get; internal set; }

    public DbSet<User> Users { get; set; }

    // public DbSet<Product>Products {get; set; }
    public DbSet<Category> Categories { get; set; }
    // public DbSet<OrderDetail> Orders {get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --------------------USER--------------------
        modelBuilder.Entity<User>().HasKey(u => u.UserID); //PK
        modelBuilder.Entity<User>().Property(u => u.UserID).IsRequired().ValueGeneratedOnAdd();

        modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(32);

        modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(20);

        modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(20);

        modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasMaxLength(10);

        modelBuilder.Entity<User>().Property(u => u.Address).HasMaxLength(255);

        // ### Relationship
        // modelBuilder.Entity<User>()
        // .HasMany(u => u.Orders)
        // .WithOne(o => o.User)
        // .HasForeignKey(o => o.UserId);

        // --------------------Category--------------------
        modelBuilder.Entity<Category>().HasKey(c => c.CategoryID); //PK
        modelBuilder.Entity<Category>().Property(c => c.CategoryID).IsRequired().ValueGeneratedOnAdd();

        modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

        // ### Relationship
        // modelBuilder.Entity<Category>()
        // .HasMany(c => c.Products)
        // .WithOne(p => p.Category)
        // .HasForeignKey(p => p.CategoryId);

        // --------------------Order--------------------
        modelBuilder.Entity<CustomerOrder>().HasKey(o => o.OrderId); //PK
        modelBuilder.Entity<CustomerOrder>().Property(o => o.OrderId).IsRequired().ValueGeneratedOnAdd();

        modelBuilder.Entity<CustomerOrder>().Property(o => o.Status).IsRequired();

        modelBuilder.Entity<CustomerOrder>().Property(o => o.Payment).IsRequired();

        // ### Relationship Many-To-Many
        // modelBuilder.Entity<CustomerOrder>()
        // .HasMany(o => o.Products)
        // .WithOne(p => p.Order)
        // .HasForeignKey(p => p.OrderId);

        // --------------------Product--------------------
        modelBuilder.Entity<Product>().HasKey(p => p.ProductID); //PK
        modelBuilder.Entity<Product>().Property(p => p.ProductID).IsRequired().ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired().HasMaxLength(50);

        modelBuilder.Entity<Product>().Property(p => p.Quantity).IsRequired();

        modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();

    }

}
