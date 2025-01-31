using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace TuristickaAgencija.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aranzman> Aranzmans { get; set; }

    public virtual DbSet<Klijent> Klijents { get; set; }

    public virtual DbSet<NacinPlacanja> NacinPlacanjas { get; set; }

    public virtual DbSet<Poslovnica> Poslovnicas { get; set; }

    public virtual DbSet<Prevoz> Prevozs { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijas { get; set; }

    public virtual DbSet<Smjestaj> Smjestajs { get; set; }

    public virtual DbSet<Termin> Termins { get; set; }

    public virtual DbSet<Usluga> Uslugas { get; set; }

    public virtual DbSet<Zaposleni> Zaposlenis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aranzman>(entity =>
        {
            entity.HasKey(e => e.IdAranzmana).HasName("PRIMARY");

            entity.ToTable("aranzman");

            entity.HasIndex(e => e.PrevozIdPrevoza, "fk_ARANZMAN_PREVOZ1_idx");

            entity.HasIndex(e => e.SmjestajIdSmjestaja, "fk_ARANZMAN_SMJESTAJ1_idx");

            entity.Property(e => e.IdAranzmana).HasColumnName("idAranzmana");
            entity.Property(e => e.Cijena)
                .HasPrecision(10, 2)
                .HasColumnName("cijena");
            entity.Property(e => e.NazivDestinacije)
                .HasMaxLength(45)
                .HasColumnName("nazivDestinacije");
            entity.Property(e => e.OsiguranjeIdOsiguranja).HasColumnName("OSIGURANJE_idOsiguranja");
            entity.Property(e => e.PrevozIdPrevoza).HasColumnName("PREVOZ_idPrevoza");
            entity.Property(e => e.SmjestajIdSmjestaja).HasColumnName("SMJESTAJ_idSmjestaja");
            entity.Property(e => e.Termin).HasColumnName("termin");
            entity.Property(e => e.Trajanje)
                .HasMaxLength(45)
                .HasColumnName("trajanje");
            entity.Property(e => e.VodicIdVodica).HasColumnName("VODIC_idVodica");

            entity.HasOne(d => d.PrevozIdPrevozaNavigation).WithMany(p => p.Aranzmen)
                .HasForeignKey(d => d.PrevozIdPrevoza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ARANZMAN_PREVOZ1");

            entity.HasOne(d => d.SmjestajIdSmjestajaNavigation).WithMany(p => p.Aranzmen)
                .HasForeignKey(d => d.SmjestajIdSmjestaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ARANZMAN_SMJESTAJ1");
        });

        modelBuilder.Entity<Klijent>(entity =>
        {
            entity.HasKey(e => e.IdKlijenta).HasName("PRIMARY");

            entity.ToTable("klijent");

            entity.Property(e => e.IdKlijenta).HasColumnName("idKlijenta");
            entity.Property(e => e.BrojTelefona)
                .HasMaxLength(45)
                .HasColumnName("brojTelefona");
            entity.Property(e => e.DatumRodjenja).HasColumnName("datumRodjenja");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Ime)
                .HasMaxLength(45)
                .HasColumnName("ime");
            entity.Property(e => e.Prezime)
                .HasMaxLength(45)
                .HasColumnName("prezime");
        });

        modelBuilder.Entity<NacinPlacanja>(entity =>
        {
            entity.HasKey(e => e.IdNacinaPlacanja).HasName("PRIMARY");

            entity.ToTable("nacin_placanja");

            entity.Property(e => e.IdNacinaPlacanja)
                .ValueGeneratedNever()
                .HasColumnName("idNacinaPlacanja");
            entity.Property(e => e.NazivNacinaPlacanja)
                .HasMaxLength(45)
                .HasColumnName("nazivNacinaPlacanja");
        });

        modelBuilder.Entity<Poslovnica>(entity =>
        {
            entity.HasKey(e => e.IdPoslovnice).HasName("PRIMARY");

            entity.ToTable("poslovnica");

            entity.HasIndex(e => e.IdPoslovnice, "idPoslovnice_UNIQUE").IsUnique();

            entity.Property(e => e.IdPoslovnice)
                .ValueGeneratedNever()
                .HasColumnName("idPoslovnice");
            entity.Property(e => e.Adresa)
                .HasMaxLength(45)
                .HasColumnName("adresa");
            entity.Property(e => e.BrojTelefona)
                .HasMaxLength(20)
                .HasColumnName("brojTelefona");
        });

        modelBuilder.Entity<Prevoz>(entity =>
        {
            entity.HasKey(e => e.IdPrevoza).HasName("PRIMARY");

            entity.ToTable("prevoz");

            entity.Property(e => e.IdPrevoza).HasColumnName("idPrevoza");
            entity.Property(e => e.Cijena)
                .HasPrecision(10, 2)
                .HasColumnName("cijena");
            entity.Property(e => e.NazivKompanije)
                .HasMaxLength(45)
                .HasColumnName("nazivKompanije");
            entity.Property(e => e.TipPrevoza)
                .HasMaxLength(45)
                .HasColumnName("tipPrevoza");
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.HasKey(e => e.IdRezervacije).HasName("PRIMARY");

            entity.ToTable("rezervacija");

            entity.HasIndex(e => e.NacinPlacanjaIdNacinaPlacanja, "fk_REZERVACIJA_NACIN_PLACANJA1_idx");

            entity.HasIndex(e => e.PoslovnicaIdPoslovnice, "fk_REZERVACIJA_POSLOVNICA1_idx");

            entity.HasIndex(e => e.IdAranzmana, "idAranzmana_idx");

            entity.HasIndex(e => e.IdKlijenta, "idKlijenta_idx");

            entity.HasIndex(e => e.IdZaposlenog, "idZaposlenog_idx");

            entity.Property(e => e.IdRezervacije).HasColumnName("idRezervacije");
            entity.Property(e => e.DatumRezervacije).HasColumnName("datumRezervacije");
            entity.Property(e => e.IdAranzmana).HasColumnName("idAranzmana");
            entity.Property(e => e.IdKlijenta).HasColumnName("idKlijenta");
            entity.Property(e => e.IdZaposlenog).HasColumnName("idZaposlenog");
            entity.Property(e => e.NacinPlacanjaIdNacinaPlacanja).HasColumnName("NACIN_PLACANJA_idNacinaPlacanja");
            entity.Property(e => e.PoslovnicaIdPoslovnice).HasColumnName("POSLOVNICA_idPoslovnice");
            entity.Property(e => e.UkupnaCijena)
                .HasPrecision(10, 2)
                .HasColumnName("ukupnaCijena");

            entity.HasOne(d => d.IdAranzmanaNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.IdAranzmana)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rezervacija_idAranzmana");

            entity.HasOne(d => d.IdKlijentaNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.IdKlijenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rezervacija_iidKlijenta");

            entity.HasOne(d => d.IdZaposlenogNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.IdZaposlenog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rezervacija_idZaposlenog");

            entity.HasOne(d => d.NacinPlacanjaIdNacinaPlacanjaNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.NacinPlacanjaIdNacinaPlacanja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_REZERVACIJA_NACIN_PLACANJA1");

            entity.HasOne(d => d.PoslovnicaIdPoslovniceNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.PoslovnicaIdPoslovnice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_REZERVACIJA_POSLOVNICA1");

            entity.HasMany(d => d.UslugaIdUsluges).WithMany(p => p.RezervacijaIdRezervacijes)
                .UsingEntity<Dictionary<string, object>>(
                    "RezervacijaHasUsluga",
                    r => r.HasOne<Usluga>().WithMany()
                        .HasForeignKey("UslugaIdUsluge")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_REZERVACIJA_has_USLUGA_USLUGA1"),
                    l => l.HasOne<Rezervacija>().WithMany()
                        .HasForeignKey("RezervacijaIdRezervacije")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_REZERVACIJA_has_USLUGA_REZERVACIJA1"),
                    j =>
                    {
                        j.HasKey("RezervacijaIdRezervacije", "UslugaIdUsluge")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("rezervacija_has_usluga");
                        j.HasIndex(new[] { "RezervacijaIdRezervacije" }, "fk_REZERVACIJA_has_USLUGA_REZERVACIJA1_idx");
                        j.HasIndex(new[] { "UslugaIdUsluge" }, "fk_REZERVACIJA_has_USLUGA_USLUGA1_idx");
                        j.IndexerProperty<int>("RezervacijaIdRezervacije").HasColumnName("REZERVACIJA_idRezervacije");
                        j.IndexerProperty<int>("UslugaIdUsluge").HasColumnName("USLUGA_idUsluge");
                    });
        });

        modelBuilder.Entity<Smjestaj>(entity =>
        {
            entity.HasKey(e => e.IdSmjestaja).HasName("PRIMARY");

            entity.ToTable("smjestaj");

            entity.Property(e => e.IdSmjestaja).HasColumnName("idSmjestaja");
            entity.Property(e => e.BrojZvjezdica)
                .HasMaxLength(45)
                .HasColumnName("brojZvjezdica");
            entity.Property(e => e.Cijena)
                .HasPrecision(10, 2)
                .HasColumnName("cijena");
            entity.Property(e => e.Lokacija)
                .HasMaxLength(45)
                .HasColumnName("lokacija");
            entity.Property(e => e.NazivSmjestaja)
                .HasMaxLength(45)
                .HasColumnName("nazivSmjestaja");
            entity.Property(e => e.Pogodnosti)
                .HasMaxLength(45)
                .HasColumnName("pogodnosti");
            entity.Property(e => e.VrstaSmjestaja)
                .HasMaxLength(45)
                .HasColumnName("vrstaSmjestaja");
        });

        modelBuilder.Entity<Termin>(entity =>
        {
            entity.HasKey(e => e.IdTermina).HasName("PRIMARY");

            entity.ToTable("termin");

            entity.HasIndex(e => e.IdAranzmana, "idAranzmana_idx");

            entity.Property(e => e.IdTermina)
                .ValueGeneratedNever()
                .HasColumnName("idTermina");
            entity.Property(e => e.BrojSlobodnihMjesta).HasColumnName("brojSlobodnihMjesta");
            entity.Property(e => e.DatumKraja).HasColumnName("datumKraja");
            entity.Property(e => e.DatumPocetka).HasColumnName("datumPocetka");
            entity.Property(e => e.IdAranzmana).HasColumnName("idAranzmana");

            entity.HasOne(d => d.IdAranzmanaNavigation).WithMany(p => p.Termins)
                .HasForeignKey(d => d.IdAranzmana)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("termin_idAranzmana");
        });

        modelBuilder.Entity<Usluga>(entity =>
        {
            entity.HasKey(e => e.IdUsluge).HasName("PRIMARY");

            entity.ToTable("usluga");

            entity.Property(e => e.IdUsluge).HasColumnName("idUsluge");
            entity.Property(e => e.Cijena)
                .HasPrecision(10, 2)
                .HasColumnName("cijena");
            entity.Property(e => e.TipUsluge)
                .HasMaxLength(45)
                .HasColumnName("tipUsluge");
        });

        modelBuilder.Entity<Zaposleni>(entity =>
        {
            entity.HasKey(e => e.IdZaposlenog).HasName("PRIMARY");

            entity.ToTable("zaposleni");

            entity.HasIndex(e => e.IdPoslovnice, "idPoslovnice_idx");

            entity.Property(e => e.IdZaposlenog).HasColumnName("idZaposlenog");
            entity.Property(e => e.IdPoslovnice).HasColumnName("idPoslovnice");
            entity.Property(e => e.Ime)
                .HasMaxLength(45)
                .HasColumnName("ime");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Lozinka)
                .HasMaxLength(45)
                .HasColumnName("lozinka");
            entity.Property(e => e.Plata)
                .HasPrecision(10, 2)
                .HasColumnName("plata");
            entity.Property(e => e.Pozicija)
                .HasMaxLength(45)
                .HasColumnName("pozicija");
            entity.Property(e => e.Prezime)
                .HasMaxLength(45)
                .HasColumnName("prezime");

            entity.HasOne(d => d.IdPoslovniceNavigation).WithMany(p => p.Zaposlenis)
                .HasForeignKey(d => d.IdPoslovnice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("zaposleni_idPoslovnice");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
