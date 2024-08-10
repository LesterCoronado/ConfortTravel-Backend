using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblAsignarRol
{
    public int IdAsignarRol { get; set; }

    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public bool Estado { get; set; }

    public virtual TblRol IdRolNavigation { get; set; } = null!;

    public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;
}
