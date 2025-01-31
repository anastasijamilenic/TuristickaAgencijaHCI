using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Zaposleni
{
    public int IdZaposlenog { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public decimal Plata { get; set; }

    public string Pozicija { get; set; } = null!;

    public int IdPoslovnice { get; set; }

    public bool IsAdmin { get; set; }

    public string Lozinka { get; set; } = null!;

    public virtual Poslovnica IdPoslovniceNavigation { get; set; } = null!;

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();

    // Simulacija baze podataka
    public static List<Zaposleni> GetZaposleni()
    {
        return new List<Zaposleni>
            {
                new Zaposleni { Ime = "Petar", Prezime = "Petrović", IsAdmin = true },
                new Zaposleni { Ime = "Jovan", Prezime = "Jovanović", IsAdmin = false }
            };
    }

    public static Zaposleni? Authenticate(string username, string password)
    {
        var zaposleni = GetZaposleni().FirstOrDefault(z => z.Ime == username && z.Lozinka == password);
        return zaposleni;
    }

}
