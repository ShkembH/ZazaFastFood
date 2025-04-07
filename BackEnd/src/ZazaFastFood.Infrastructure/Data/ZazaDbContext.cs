namespace ZazaFastFood.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ZazaFastFood.Domain.Entities;

public class ZazaDbContext : DbContext
{
    public ZazaDbContext(DbContextOptions<ZazaDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<MenuItem> MenuItems { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}