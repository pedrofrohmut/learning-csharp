using BulkyWeb.Utils;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public DbSet<CategoryModel> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
	: base (options)
    {
	this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = new MyUtils().GetConnectionString(configuration);
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryModel>().HasData(
	    new CategoryModel { CategoryId = 1, Name = "Action",  DisplayOrder = 1 },
	    new CategoryModel { CategoryId = 2, Name = "SciFi",   DisplayOrder = 2 },
	    new CategoryModel { CategoryId = 3, Name = "History", DisplayOrder = 3 }
	);
    }
}
