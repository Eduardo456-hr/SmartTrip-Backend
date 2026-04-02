using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Data;

public partial class SmartTripContext : DbContext
{
    public SmartTripContext()
    {
    }

    public SmartTripContext(DbContextOptions<SmartTripContext> options)
        : base(options)
    {
    }

    // 1. Agregamos el nuevo DbSet para la tabla maestra
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Conductore> Conductores { get; set; }
    public virtual DbSet<Pasajero> Pasajeros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        // 2. Configuración de la Nueva Tabla USUARIOS
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("usuarios"); // en minúsculas como tu script

            entity.HasIndex(e => e.Correo, "Correo").IsUnique();

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Rol).HasMaxLength(20);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
        });

        // 3. Configuración de CONDUCTORES (Relación 1:1)
        modelBuilder.Entity<Conductore>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PRIMARY"); // UsuarioId es la PK

            entity.ToTable("conductores");

            entity.HasIndex(e => e.NumeroLicencia, "NumeroLicencia").IsUnique();

            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(15);
            entity.Property(e => e.NumeroLicencia).HasMaxLength(30);

            // Configurar la relación 1 a 1 con Usuarios
            entity.HasOne(d => d.Usuario)
                .WithOne(p => p.Conductor)
                .HasForeignKey<Conductore>(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Conductor_Usuario");
        });

        // 4. Configuración de PASAJEROS (Relación 1:1)
        modelBuilder.Entity<Pasajero>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PRIMARY"); // UsuarioId es la PK

            entity.ToTable("pasajeros");

            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(15);

            // Configurar la relación 1 a 1 con Usuarios
            entity.HasOne(d => d.Usuario)
                .WithOne(p => p.Pasajero)
                .HasForeignKey<Pasajero>(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Pasajero_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}