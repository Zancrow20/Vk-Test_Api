using Microsoft.EntityFrameworkCore;
using VkWebApi.DataAccess.Models;

namespace VkWebApi.DataAccess.VkDbContext;

public sealed class VkDbContext : DbContext
{
    public DbSet<User?> Users { get; set; }

    public VkDbContext(DbContextOptions<VkDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        new UserStateConfiguration().Configure(modelBuilder.Entity<UserState>());
        new UserGroupConfiguration().Configure(modelBuilder.Entity<UserGroup>());
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
    }
}