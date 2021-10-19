using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Football_Manager.Models;

#nullable disable

namespace Football_Manager.Data
{
    public partial class FootballDbContext : DbContext
    {
        public FootballDbContext()
        {
        }

        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
