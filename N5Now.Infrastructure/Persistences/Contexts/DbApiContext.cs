using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using N5Now.Domain.Entities;

namespace N5Now.Infrastructure.Persistences.Contexts;

public partial class DbApiContext : DbContext
{
    public DbApiContext()
    {
    }

    public DbApiContext(DbContextOptions<DbApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<PermissionType> PermissionTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
