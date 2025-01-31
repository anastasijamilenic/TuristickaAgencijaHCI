using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Rezervacija
{
    public int IdRezervacije { get; set; }

    public DateOnly DatumRezervacije { get; set; }

    public decimal UkupnaCijena { get; set; }

    public int IdKlijenta { get; set; }

    public int IdAranzmana { get; set; }

    public int IdZaposlenog { get; set; }

    public int NacinPlacanjaIdNacinaPlacanja { get; set; }

    public int PoslovnicaIdPoslovnice { get; set; }

    public virtual Aranzman IdAranzmanaNavigation { get; set; } = null!;

    public virtual Klijent IdKlijentaNavigation { get; set; } = null!;

    public virtual Zaposleni IdZaposlenogNavigation { get; set; } = null!;

    public virtual NacinPlacanja NacinPlacanjaIdNacinaPlacanjaNavigation { get; set; } = null!;

    public virtual Poslovnica PoslovnicaIdPoslovniceNavigation { get; set; } = null!;

    public virtual ICollection<Usluga> UslugaIdUsluges { get; set; } = new List<Usluga>();
}
