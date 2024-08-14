using System;
using System.Collections.Generic;
using GestorControlRemoto.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorControlRemoto.Shared.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compatible> Compatible { get; set; }

    public virtual DbSet<ControlRemoto> ControlRemoto { get; set; }

    public virtual DbSet<Marca> Marca { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=ImportControlRemoto;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compatible>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compatib__3214EC0746A522BA");

            entity.HasOne(d => d.IdControlNavigation).WithMany(p => p.Compatible)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compatible_ControlRemoto");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Compatible)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compatible_Marca");
        });

        modelBuilder.Entity<ControlRemoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ControlR__3214EC078A0C9833");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marca__3214EC072E1BC00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
