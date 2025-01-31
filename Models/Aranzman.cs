using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Data;

public partial class Aranzman
{
    public int IdAranzmana { get; set; }

    public string NazivDestinacije { get; set; } = null!;

    public decimal Cijena { get; set; }

    public DateOnly Termin { get; set; }

    public string Trajanje { get; set; } = null!;

    public int VodicIdVodica { get; set; }

    public int OsiguranjeIdOsiguranja { get; set; }

    public int PrevozIdPrevoza { get; set; }

    public int SmjestajIdSmjestaja { get; set; }

    public virtual Prevoz PrevozIdPrevozaNavigation { get; set; } = null!;

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();

    public virtual Smjestaj SmjestajIdSmjestajaNavigation { get; set; } = null!;

    public virtual ICollection<Termin> Termins { get; set; } = new List<Termin>();
}
