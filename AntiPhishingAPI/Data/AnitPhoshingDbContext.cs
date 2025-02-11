using LearningASPweb.Configurations.DataConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningASPweb.Data;

public class AnitPhoshingDbContext:IdentityDbContext<APIUserModel>
{
    public AnitPhoshingDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfig());
    }
}