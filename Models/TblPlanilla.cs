using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblPlanilla
{
    public int IdPlanilla { get; set; }

    public int IdUsuario { get; set; }

    public int Dpi { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string EstadoCivil { get; set; } = null!;

    public DateTime FechaContratacion { get; set; }

    public string TiempoDeContrato { get; set; } = null!;

    public int? IdCargo { get; set; }

    public double SalarioBase { get; set; }

    public int? HorasTrabajadas { get; set; }

    public double? TarifaPorHora { get; set; }

    public bool Estado { get; set; }

    public virtual TblCargoLaboral? IdCargoNavigation { get; set; }

    public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<TblBonoPlanilla> TblBonoPlanillas { get; set; } = new List<TblBonoPlanilla>();

    public virtual ICollection<TblDescuentoPlanilla> TblDescuentoPlanillas { get; set; } = new List<TblDescuentoPlanilla>();

    public virtual ICollection<TblPagoPlanilla> TblPagoPlanillas { get; set; } = new List<TblPagoPlanilla>();
}
