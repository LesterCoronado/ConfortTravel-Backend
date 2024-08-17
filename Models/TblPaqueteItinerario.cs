using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblPaqueteItinerario
{
    public int IdPaqueteItinerario { get; set; }

    public int IdPaqueteViaje { get; set; }

    public string Actividad { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Horario { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]

    public virtual TblPaqueteViaje? IdPaqueteViajeNavigation { get; set; } = null!;
}
