using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblOrdenDePago
{
    public int IdOrdenDePago { get; set; }

    public int IdCotizacion { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaGenerado { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public bool Estado { get; set; }

    public virtual TblCotizacion IdCotizacionNavigation { get; set; } = null!;

    public virtual ICollection<TblFel> TblFels { get; set; } = new List<TblFel>();
}
