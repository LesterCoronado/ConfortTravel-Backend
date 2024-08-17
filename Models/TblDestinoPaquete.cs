using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblDestinoPaquete
{
    public int IdDestinoPaquete { get; set; }

    public int IdPaquete { get; set; }

    public int IdDestino { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblDestino? IdDestinoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TblPaqueteViaje? IdPaqueteNavigation { get; set; } = null!;
}
