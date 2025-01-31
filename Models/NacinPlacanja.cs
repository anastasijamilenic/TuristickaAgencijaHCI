using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class NacinPlacanja
{
    public int IdNacinaPlacanja { get; set; }

    public string NazivNacinaPlacanja { get; set; } = null!;

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();
}
