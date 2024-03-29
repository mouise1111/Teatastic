﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    public DbSet<Brand> Brands { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<TeatasticUser>
{
    public void Configure(EntityTypeBuilder<TeatasticUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}