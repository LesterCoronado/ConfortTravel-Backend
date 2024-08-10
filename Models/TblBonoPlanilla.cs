using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblBonoPlanilla
{
    public int IdBonoPlanilla { get; set; }

    public int IdPlanilla { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public bool Estado { get; set; }

    public virtual TblBono IdBonoPlanillaNavigation { get; set; } = null!;

    public virtual TblPlanilla IdPlanillaNavigation { get; set; } = null!;
}
