using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblItinerario
{
    public int IdItinerario { get; set; }

    public int IdDestino { get; set; }

    public int? IdVehiculo { get; set; }

    public DateTime FechaSalida { get; set; }

    public TimeSpan? HoraSalida { get; set; }

    public DateTime FechaRetorno { get; set; }

    public TimeSpan? HoraRetorno { get; set; }

    public string TotalDias { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual TblDestino IdDestinoNavigation { get; set; } = null!;

    public virtual TblVehiculo? IdVehiculoNavigation { get; set; }

    public virtual ICollection<TblItinerarioHotel> TblItinerarioHotels { get; set; } = new List<TblItinerarioHotel>();

    public virtual ICollection<TblItinerarioPasajero> TblItinerarioPasajeros { get; set; } = new List<TblItinerarioPasajero>();
}
