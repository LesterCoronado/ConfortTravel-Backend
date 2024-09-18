using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual TblCargoLaboral? IdCargoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblEmpleado? IdEmpleadoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<TblBonoPlanilla>? TblBonoPlanillas { get; set; } = new List<TblBonoPlanilla>();

    [JsonIgnore]
    public virtual ICollection<TblDescuentoPlanilla>? TblDescuentoPlanillas { get; set; } = new List<TblDescuentoPlanilla>();

    [JsonIgnore]
    public virtual ICollection<TblPagoPlanilla>? TblPagoPlanillas { get; set; } = new List<TblPagoPlanilla>();
}
