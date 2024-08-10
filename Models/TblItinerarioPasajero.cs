using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblItinerarioPasajero
{
    public int IdPasajeroItinerario { get; set; }

    public int IdPersona { get; set; }

    public int IdItinerario { get; set; }

    public bool Estado { get; set; }

    public virtual TblItinerario IdItinerarioNavigation { get; set; } = null!;

    public virtual TblPersona IdPersonaNavigation { get; set; } = null!;
}
