using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Skinet.Core.Entities;
using Skinet.Infrastructure.Utils;

namespace Skinet.Infrastructure.Data;

public class StoreDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public DbSet<Product>? Products { get; set; }

    public StoreDbContext(DbContextOptions<StoreDbContext> options, IConfiguration configuration)
    : base (options)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = MyUtils.GetConnectionString(this.configuration);
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var product1 = new Product() { Id = 1, Name = "Product 1" };
        var product2 = new Product() { Id = 2, Name = "Product 2" };
        var product3 = new Product() { Id = 3, Name = "Product 3" };
        modelBuilder.Entity<Product>().HasData(product1, product2, product3);
    }
}
