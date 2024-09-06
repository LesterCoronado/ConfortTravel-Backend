using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblReserva
{
    public int IdReserva { get; set; }

    public int IdPaqueteViaje { get; set; }

    public int? IdVehiculo { get; set; }

    public DateTime FechaSalida { get; set; }

    public String? HoraSalida { get; set; }

    public DateTime FechaRetorno { get; set; }

    public String? HoraRetorno { get; set; }

    public int TotalDias { get; set; }

    public string? Observaciones { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]

    public virtual TblPaqueteViaje? IdPaqueteViajeNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblVehiculo? IdVehiculoNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblReservaAlojamiento>? TblReservaAlojamientos { get; set; } = new List<TblReservaAlojamiento>();
}
