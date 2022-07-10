using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaveToDateApp.Entities;

namespace SaveToDateApp.Database;

public class AppDbContext:IdentityDbContext
{
    public DbSet<FridgeProductEntity> FridgeProducts { get; set; }
    public DbSet<PantryProductEntity> PantryProducts { get; set; }
    
    
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
}