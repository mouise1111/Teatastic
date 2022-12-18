using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Teatastic.Areas.Identity.Data;
using Teatastic.Models;

namespace Teatastic.Data;

public class TeatasticContext : IdentityDbContext<TeatasticUser>
{
    public TeatasticContext(DbContextOptions<TeatasticContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tea_Function>().HasKey(tf => new
        {
            tf.TeaId,
            tf.FunctionId
        });

        builder.Entity<Tea_Function>().HasOne(f => f.Function).WithMany(tf => tf.Teas_Functions).HasForeignKey(f => f.FunctionId);
        builder.Entity<Tea_Function>().HasOne(f => f.Tea).WithMany(tf => tf.Teas_Functions).HasForeignKey(f => f.TeaId);
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Tea> Teas { get; set; }
    public DbSet<Function> Functions { get; set; }
}
