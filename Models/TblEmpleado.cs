using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblEmpleado
{
    public int IdEmpleado { get; set; }

    public int IdPersona { get; set; }

    public DateTime FechaNac { get; set; }

    public string EstadoCivil { get; set; } = null!;

    public string FormacionAcademica { get; set; } = null!;

    public string? TipoSangre { get; set; }

    public int? Altura { get; set; }

    public int? Peso { get; set; }

    public bool Estado { get; set; }

    public virtual TblPersona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<TblPlanilla> TblPlanillas { get; set; } = new List<TblPlanilla>();
}
