using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblPasajeroReserva
{
    public int IdPasajeroReserva { get; set; }

    public int IdPersona { get; set; }

    public int IdReserva { get; set; }

    public bool Estado { get; set; }

    public virtual TblPersona IdPersonaNavigation { get; set; } = null!;
}
