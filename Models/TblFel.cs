﻿using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblFel
{
    public int IdFel { get; set; }

    public int IdOrdenDePago { get; set; }

    public int IdUsuario { get; set; }

    public int? NumeroFactura { get; set; }

    public string? Serie { get; set; }

    public int? Nit { get; set; }

    public double Subtotal { get; set; }

    public string? Moneda { get; set; }

    public int? IdImpuesto { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public bool Estado { get; set; }

    public virtual TblImpuesto? IdImpuestoNavigation { get; set; }

    public virtual TblOrdenDePago IdOrdenDePagoNavigation { get; set; } = null!;

    public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;
}
