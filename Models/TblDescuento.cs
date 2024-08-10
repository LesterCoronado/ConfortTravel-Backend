using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblDescuento
{
    public int IdDescuento { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double Monto { get; set; }

    public string? Moneda { get; set; }

    public string FrecuenciaDescuento { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual TblDescuentoPlanilla? TblDescuentoPlanilla { get; set; }
}
