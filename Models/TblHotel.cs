using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblHotel
{
    public int IdHotel { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Telefono { get; set; }

    public string? Pais { get; set; }

    public string? Depto { get; set; }

    public string? Direccion { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblReservaAlojamiento>? TblReservaAlojamientos { get; set; } = new List<TblReservaAlojamiento>();
}
