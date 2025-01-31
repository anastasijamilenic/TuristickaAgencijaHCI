using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Termin
{
    public int IdTermina { get; set; }

    public DateOnly DatumPocetka { get; set; }

    public DateOnly DatumKraja { get; set; }

    public int BrojSlobodnihMjesta { get; set; }

    public int IdAranzmana { get; set; }

    public virtual Aranzman IdAranzmanaNavigation { get; set; } = null!;
}
