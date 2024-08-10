using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblUsuario
{
    public int IdUsuario { get; set; }

    public int IdPersona { get; set; }

    public string Contraseña { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual TblPersona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<TblAsignarRol> TblAsignarRols { get; set; } = new List<TblAsignarRol>();

    public virtual ICollection<TblCotizacion> TblCotizacions { get; set; } = new List<TblCotizacion>();

    public virtual ICollection<TblFel> TblFels { get; set; } = new List<TblFel>();

    public virtual ICollection<TblPlanilla> TblPlanillas { get; set; } = new List<TblPlanilla>();
}
