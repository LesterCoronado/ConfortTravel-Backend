using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblReservaAlojamiento
{
    public int IdReservaAlojamiento { get; set; }

    public int IdReserva { get; set; }

    public int IdHotel { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public int? TotalDias { get; set; } 

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblHotel? IdHotelNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblReserva? IdReservaNavigation { get; set; } = null!;
}
