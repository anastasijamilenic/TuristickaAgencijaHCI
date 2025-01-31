using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Prevoz
{
    public int IdPrevoza { get; set; }

    public string TipPrevoza { get; set; } = null!;

    public string NazivKompanije { get; set; } = null!;

    public decimal Cijena { get; set; }

    public virtual ICollection<Aranzman> Aranzmen { get; set; } = new List<Aranzman>();
}
