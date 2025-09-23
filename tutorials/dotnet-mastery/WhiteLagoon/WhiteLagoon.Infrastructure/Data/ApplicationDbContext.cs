using Microsoft.EntityFrameworkCore;

using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        seedVillas(modelBuilder);
        seedVillaNumbers(modelBuilder);
    }

    private void seedVillas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name = "Royal Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x400",
                Occupancy = 4,
                Price = 200,
                Sqft = 550,
            },
            new Villa
            {
                Id = 2,
                Name = "Premium Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x401",
                Occupancy = 4,
                Price = 300,
                Sqft = 550,
            },
            new Villa
            {
                Id = 3,
                Name = "Luxury Pool Villa",
                Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                ImageUrl = "https://placehold.co/600x402",
                Occupancy = 4,
                Price = 400,
                Sqft = 750,
            }
        );
    }

    private void seedVillaNumbers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VillaNumber>().HasData(
            new VillaNumber
            {
                Id = 1,
                Number = 101,
                VillaId = 1,
            },
            new VillaNumber
            {
                Id = 2,
                Number = 102,
                VillaId = 1,
            },
            new VillaNumber
            {
                Id = 3,
                Number = 103,
                VillaId = 1,
            },
            new VillaNumber
            {
                Id = 4,
                Number = 104,
                VillaId = 1,
            },
            new VillaNumber
            {
                Id = 5,
                Number = 201,
                VillaId = 2,
            },
            new VillaNumber
            {
                Id = 6,
                Number = 202,
                VillaId = 2,
            },
            new VillaNumber
            {
                Id = 7,
                Number = 203,
                VillaId = 2,
            },
            new VillaNumber
            {
                Id = 8,
                Number = 301,
                VillaId = 3,
            },
            new VillaNumber
            {
                Id = 9,
                Number = 302,
                VillaId = 3,
            }
        );
    }
}
