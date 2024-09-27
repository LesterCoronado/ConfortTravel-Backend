using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblBonoPlanilla
{
    public int IdBonoPlanilla { get; set; }

    public int IdBono { get; set; }

    public int IdPlanilla { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblBono? IdBonoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblPlanilla? IdPlanillaNavigation { get; set; } = null!;
}
