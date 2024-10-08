﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblCotizacion
{
    public int IdCotizacion { get; set; }

    public int IdUsuario { get; set; }

    public int IdPaqueteViaje { get; set; }

    public DateTime FechaSalida { get; set; }

    public int TotalAdultos { get; set; }

    public int TotalNinos { get; set; }

    public string? Comentario { get; set; }

    public double? PrecioCotizacion { get; set; }

    public DateTime ValidoHasta { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblPaqueteViaje? IdPaqueteViajeNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblUsuario? IdUsuarioNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<TblOrdenDePago>? TblOrdenDePagos { get; set; } = new List<TblOrdenDePago>();
}
