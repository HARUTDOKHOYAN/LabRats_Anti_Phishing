using AntiPhishingAPI.Data.Models;
using LearningASPweb.Configurations.DataConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningASPweb.Data;

public class AnitPhoshingDbContext:IdentityDbContext<APIUserModel>
{
    public DbSet<DbData> Links { get; set; } = null!;
    public AnitPhoshingDbContext(DbContextOptions options) : base(options)
    {
       Database.EnsureCreatedAsync();   
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfig());
    }
}