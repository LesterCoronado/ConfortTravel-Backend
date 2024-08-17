using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblPlanilla
{
    public int IdPlanilla { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime FechaContratacion { get; set; }

    public DateTime? FechaDeBaja { get; set; }

    public string TipoContrato { get; set; } = null!;

    public int? TiempoContrato { get; set; }

    public int IdCargo { get; set; }

    public double SalarioBase { get; set; }

    public int NoCuenta { get; set; }

    public bool Estado { get; set; }

    public virtual TblCargoLaboral IdCargoNavigation { get; set; } = null!;

    public virtual TblEmpleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual ICollection<TblBonoPlanilla> TblBonoPlanillas { get; set; } = new List<TblBonoPlanilla>();

    public virtual ICollection<TblDescuentoPlanilla> TblDescuentoPlanillas { get; set; } = new List<TblDescuentoPlanilla>();

    public virtual ICollection<TblPagoPlanilla> TblPagoPlanillas { get; set; } = new List<TblPagoPlanilla>();
}
