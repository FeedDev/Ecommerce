using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Ecommerce.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Carrello> Carrello { get; set; }
        public virtual DbSet<CategoriaProdotto> CategoriaProdotto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<IndirizzoSpedizione> IndirizzoSpedizione { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<MetodoPagamento> MetodoPagamento { get; set; }
        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<Prodotto> Prodotto { get; set; }
        public virtual DbSet<RecordOrdine> RecordOrdine { get; set; }
        public virtual DbSet<MetodoSpedizione> MetodoSpedizione { get; set; }
        public virtual DbSet<CodiceSconto> CodiceSconto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrello>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Carrello)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Carrello>()
                .HasMany(e => e.RecordOrdine)
                .WithRequired(e => e.Carrello)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoriaProdotto>()
                .HasMany(e => e.Prodotto)
                .WithRequired(e => e.CategoriaProdotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Carrello)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.IndirizzoSpedizione)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.MetodoPagamento)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IndirizzoSpedizione>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.IndirizzoSpedizione)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Ruolo)
                .IsFixedLength();

            modelBuilder.Entity<MetodoPagamento>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.MetodoPagamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ordine>()
                .Property(e => e.TotaleOrdine)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotto>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotto>()
                .Property(e => e.PrezzoPrecedente)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotto>()
                .Property(e => e.FotoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Prodotto>()
                .HasMany(e => e.RecordOrdine)
                .WithRequired(e => e.Prodotto)
                .WillCascadeOnDelete(false);
        }
    }
}
