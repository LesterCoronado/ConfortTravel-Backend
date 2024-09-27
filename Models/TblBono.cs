using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblBono
{
    public int IdBono { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double Monto { get; set; }

    public string? FrecuenciaPago { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblBonoPlanilla? TblBonoPlanilla { get; set; }
}
