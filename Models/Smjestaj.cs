using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Smjestaj
{
    public int IdSmjestaja { get; set; }

    public string NazivSmjestaja { get; set; } = null!;

    public string VrstaSmjestaja { get; set; } = null!;

    public string Lokacija { get; set; } = null!;

    public string BrojZvjezdica { get; set; } = null!;

    public string Pogodnosti { get; set; } = null!;

    public decimal Cijena { get; set; }

    public virtual ICollection<Aranzman> Aranzmen { get; set; } = new List<Aranzman>();
}
