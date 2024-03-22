using Bloggie.Mvc.Models.Domain;
using Bloggie.Mvc.Utils;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Mvc.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    : base(options)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = new MyUtils().GetConnectionString(configuration);
        optionsBuilder.UseNpgsql(connectionString);
    }
}
