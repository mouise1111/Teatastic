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
    public DbSet<Teatastic.Models.Function> Function { get; set; }
    public DbSet<Teatastic.Models.Tea> Tea { get; set; }

}
