using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblReserva
{
    public int IdReserva { get; set; }

    public int IdPaqueteViaje { get; set; }

    public int? IdVehiculo { get; set; }

    public DateTime FechaSalida { get; set; }

    public TimeSpan? HoraSalida { get; set; }

    public DateTime FechaRetorno { get; set; }

    public TimeSpan? HoraRetorno { get; set; }

    public int TotalDias { get; set; }

    public string? Observaciones { get; set; }

    public bool Estado { get; set; }

    public virtual TblPaqueteViaje IdPaqueteViajeNavigation { get; set; } = null!;

    public virtual TblVehiculo? IdVehiculoNavigation { get; set; }

    public virtual ICollection<TblReservaAlojamiento> TblReservaAlojamientos { get; set; } = new List<TblReservaAlojamiento>();
}
