using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblImpuesto
{
    public int IdImpuesto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public double Porcentaje { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TblFel> TblFels { get; set; } = new List<TblFel>();
}
