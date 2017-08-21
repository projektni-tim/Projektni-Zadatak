namespace ProjektniZadatak.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProjektniZadatakContext : DbContext
    {
        public ProjektniZadatakContext()
            : base("name=ProjektniZadatakContext")
        {
        }

        public virtual DbSet<Adresa> Adresa { get; set; }
        public virtual DbSet<EmailAdresa> EmailAdresa { get; set; }
        public virtual DbSet<FiksniTelefon> FiksniTelefon { get; set; }
        public virtual DbSet<Grad> Grad { get; set; }
        public virtual DbSet<LokalFiksni> LokalFiksni { get; set; }
        public virtual DbSet<LokalMobilni> LokalMobilni { get; set; }
        public virtual DbSet<MobilniTelefon> MobilniTelefon { get; set; }
        public virtual DbSet<Opstina> Opstina { get; set; }
        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Pol> Pol { get; set; }
        public virtual DbSet<TipAdrese> TipAdrese { get; set; }
        public virtual DbSet<TipEmailAdrese> TipEmailAdrese { get; set; }
        public virtual DbSet<TipFiskni> TipFiskni { get; set; }
        public virtual DbSet<TipMobilni> TipMobilni { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grad>()
                .HasMany(e => e.Adresa)
                .WithRequired(e => e.Grad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Grad>()
                .HasMany(e => e.Opstina)
                .WithRequired(e => e.Grad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Opstina>()
                .HasMany(e => e.Osoba)
                .WithOptional(e => e.Opstina)
                .HasForeignKey(e => e.OpstinaRodjenjaId);

            modelBuilder.Entity<Osoba>()
                .Property(e => e.Beleska)
                .IsUnicode(false);

            modelBuilder.Entity<Osoba>()
                .HasMany(e => e.Adresa)
                .WithRequired(e => e.Osoba)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Osoba>()
                .HasMany(e => e.EmailAdresa)
                .WithRequired(e => e.Osoba)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Osoba>()
                .HasMany(e => e.FiksniTelefon)
                .WithRequired(e => e.Osoba)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Osoba>()
                .HasMany(e => e.MobilniTelefon)
                .WithRequired(e => e.Osoba)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipAdrese>()
                .HasMany(e => e.Adresa)
                .WithRequired(e => e.TipAdrese)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipEmailAdrese>()
                .HasMany(e => e.EmailAdresa)
                .WithRequired(e => e.TipEmailAdrese)
                .WillCascadeOnDelete(false);
        }
    }
}
