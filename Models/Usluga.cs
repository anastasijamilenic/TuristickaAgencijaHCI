using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Usluga
{
    public int IdUsluge { get; set; }

    public string TipUsluge { get; set; } = null!;

    public decimal Cijena { get; set; }

    public virtual ICollection<Rezervacija> RezervacijaIdRezervacijes { get; set; } = new List<Rezervacija>();
}
