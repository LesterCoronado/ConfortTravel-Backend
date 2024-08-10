using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblPagoPlanilla
{
    public int IdPagoPlanilla { get; set; }

    public int IdPlanilla { get; set; }

    public DateTime Fecha { get; set; }

    public double Monto { get; set; }

    public string Moneda { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual TblPlanilla IdPlanillaNavigation { get; set; } = null!;
}
