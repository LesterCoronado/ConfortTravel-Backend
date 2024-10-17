using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblFel
{
    public int IdFel { get; set; }

    public int IdOrdenDePago { get; set; }

    public int IdUsuario { get; set; }

    public int? NumeroFactura { get; set; }

    public string? Serie { get; set; }

    public string? Dte { get; set; }

    public int? Nit { get; set; }

    public double Subtotal { get; set; }

    public string? Moneda { get; set; }



    public int? IdImpuesto { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblImpuesto? IdImpuestoNavigation { get; set; }

    [JsonIgnore]
    public virtual TblOrdenDePago? IdOrdenDePagoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblUsuario? IdUsuarioNavigation { get; set; } = null!;
}
