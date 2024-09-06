using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblPasajeroReserva
{
    public int IdPasajeroReserva { get; set; }

    public int IdPersona { get; set; }

    public int IdReserva { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblPersona? IdPersonaNavigation { get; set; } = null!;
}
