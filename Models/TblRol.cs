using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblRol
{
    public int IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<TblAsignarRol> TblAsignarRols { get; set; } = new List<TblAsignarRol>();
}
