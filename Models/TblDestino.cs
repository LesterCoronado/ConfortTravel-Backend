using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblDestino
{
    public int IdDestino { get; set; }

    public string Nombre { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Depto { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public bool Estado { get; set; }


    [JsonIgnore]
    public virtual ICollection<TblDestinoPaquete>? TblDestinoPaquetes { get; set; } = new List<TblDestinoPaquete>();
}
