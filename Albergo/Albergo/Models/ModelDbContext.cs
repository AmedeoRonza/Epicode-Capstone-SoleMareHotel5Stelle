using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Albergo.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Camere> Camere { get; set; }
        public virtual DbSet<Galleria> Galleria { get; set; }
        public virtual DbSet<Prenotazioni> Prenotazioni { get; set; }
        public virtual DbSet<PrenotazioniServizi> PrenotazioniServizi { get; set; }
        public virtual DbSet<Recensioni> Recensioni { get; set; }
        public virtual DbSet<Servizi> Servizi { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camere>()
                .Property(e => e.Prezzo)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Prenotazioni>()
                .HasMany(e => e.PrenotazioniServizi)
                .WithRequired(e => e.Prenotazioni)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrenotazioniServizi>()
                .Property(e => e.Prezzo)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Servizi>()
                .Property(e => e.Prezzo)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Servizi>()
                .HasMany(e => e.PrenotazioniServizi)
                .WithRequired(e => e.Servizi)
                .WillCascadeOnDelete(false);
        }
    }
}
