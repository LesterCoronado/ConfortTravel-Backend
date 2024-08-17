using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblSalidum
{
    public int IdSalida { get; set; }

    public string Direccion { get; set; } = null!;

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblPaqueteViaje>? TblPaqueteViajes { get; set; } = new List<TblPaqueteViaje>();
}
