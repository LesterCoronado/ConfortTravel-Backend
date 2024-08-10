using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblItinerarioHotel
{
    public int IdItinerarioHotel { get; set; }

    public int IdItinerario { get; set; }

    public int IdHotel { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string TotalDias { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual TblHotel IdHotelNavigation { get; set; } = null!;

    public virtual TblItinerario IdItinerarioNavigation { get; set; } = null!;
}
