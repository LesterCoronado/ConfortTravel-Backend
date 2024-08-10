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
    public virtual ICollection<TblCotizacion>? TblCotizacions { get; set; } = new List<TblCotizacion>();

    [JsonIgnore]
    public virtual ICollection<TblItinerario>? TblItinerarios { get; set; } = new List<TblItinerario>();

    [JsonIgnore]
    public virtual ICollection<TblSalidaDestino>? TblSalidaDestinos { get; set; } = new List<TblSalidaDestino>();

}
