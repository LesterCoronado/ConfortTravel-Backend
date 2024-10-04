using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblDescuento
{
    public int IdDescuento { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double Monto { get; set; }

    //public string FrecuenciaDescuento { get; set; } = null!;

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblDescuentoPlanilla? TblDescuentoPlanilla { get; set; }
}
