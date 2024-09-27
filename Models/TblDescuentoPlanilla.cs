using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblDescuentoPlanilla
{
    public int IdDescuentoPlanilla { get; set; }

    public int IdDescuento { get; set; }

    public int IdPlanilla { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblDescuento? IdDescuentoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblPlanilla? IdPlanillaNavigation { get; set; } = null!;
}
