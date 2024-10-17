using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblOrdenDePago
{
    public int IdOrdenDePago { get; set; }

    public int IdCotizacion { get; set; }

    public DateTime FechaGenerado { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public string Checkout { get; set; } = null!;

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblCotizacion? IdCotizacionNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<TblFel>? TblFels { get; set; } = new List<TblFel>();
}
