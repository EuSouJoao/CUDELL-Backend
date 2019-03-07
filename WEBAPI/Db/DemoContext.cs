using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WEBAPI.Models;

namespace WEBAPI.Db
{
    public partial class DemoContext : DbContext
    {
        public DemoContext()
        {
        }

        public DemoContext(DbContextOptions options) : base(options)
        {
        }


        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<EstadoFatura> EstadoFatura { get; set; }
        public virtual DbSet<Fatura> Fatura { get; set; }
        public virtual DbSet<Fornecedor> Fornecedor { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                throw (new Exception("Configuration not Done"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "db_owner");

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit", "dbo");

                entity.Property(e => e.AuditId).HasColumnName("AuditID");

                entity.Property(e => e.Operation)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EstadoFatura>(entity =>
            {
                entity.ToTable("EstadoFatura", "dbo");

                entity.Property(e => e.EstadoFaturaId).HasColumnName("EstadoFaturaID");

                entity.Property(e => e.DescritivoEstadoFatura)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Fatura>(entity =>
            {
                entity.ToTable("Fatura", "dbo");

                entity.Property(e => e.FaturaId).HasColumnName("FaturaID");

                entity.Property(e => e.AlterUser)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EstadoFaturaId).HasColumnName("EstadoFaturaID");

                entity.Property(e => e.FornecedorId).HasColumnName("FornecedorID");

                entity.Property(e => e.InsertUser)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ResponsavelFatura).HasMaxLength(20);

                entity.HasOne(d => d.EstadoFatura)
                    .WithMany(p => p.Fatura)
                    .HasForeignKey(d => d.EstadoFaturaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fatura_Fatura");

                entity.HasOne(d => d.Fornecedor)
                    .WithMany(p => p.Fatura)
                    .HasForeignKey(d => d.FornecedorId)
                    .HasConstraintName("FK_Fatura_Fornecedor");
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.ToTable("Fornecedor", "dbo");

                entity.Property(e => e.FornecedorId)
                    .HasColumnName("FornecedorID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DescritivoFornecedor)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.FornecedorNavigation)
                    .WithOne(p => p.InverseFornecedorNavigation)
                    .HasForeignKey<Fornecedor>(d => d.FornecedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fornecedor_Fornecedor");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory", "dbo");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });
        }
    }
}
