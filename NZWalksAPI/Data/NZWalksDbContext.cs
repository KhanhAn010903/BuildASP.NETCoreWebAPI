using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var difficulties = new List<Difficulty>()
        {
            new Difficulty(){
                Id = Guid.Parse("69147972-846c-4356-aa81-68182e655b45"),
                Name = "Easy"
            },
            new Difficulty(){
                Id = Guid.Parse("3c74f516-2302-4122-af13-8362ccd2e031"),
                Name = "Medium"
            },
            new Difficulty(){
                Id = Guid.Parse("74ab0e6a-162a-48e1-a0b2-9574b59e125d"),
                Name = "Hard"
            }
        };
        modelBuilder.Entity<Difficulty>().HasData(difficulties);
    }
}