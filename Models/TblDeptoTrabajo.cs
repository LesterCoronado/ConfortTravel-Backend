using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblDeptoTrabajo
{
    public int IdDeptoTrabajo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TblCargoLaboral> TblCargoLaborals { get; set; } = new List<TblCargoLaboral>();
}
