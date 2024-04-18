using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Utils;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    : base (options)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = new MyUtils().GetConnectionString(this.configuration);
        optionsBuilder.UseNpgsql(connectionString);
    }

}
