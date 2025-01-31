using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Klijent
{
    public int IdKlijenta { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;
    public string ImePrezime => $"{Ime} {Prezime}";
    public string BrojTelefona { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly DatumRodjenja { get; set; }

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();
}
