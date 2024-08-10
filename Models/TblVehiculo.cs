using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblVehiculo
{
    public int IdVehiculo { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public string Placa { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int CapacidadPasajeros { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TblItinerario> TblItinerarios { get; set; } = new List<TblItinerario>();
}
