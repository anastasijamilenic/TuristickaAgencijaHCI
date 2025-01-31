using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Poslovnica
{
    public int IdPoslovnice { get; set; }

    public string BrojTelefona { get; set; } = null!;

    public string Adresa { get; set; } = null!;

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();

    public virtual ICollection<Zaposleni> Zaposlenis { get; set; } = new List<Zaposleni>();
}
