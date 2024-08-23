using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblPaqueteViaje
{
    public int IdPaqueteViaje { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Portada { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? ModalidadPaquete { get; set; }

    public int IdSalida { get; set; }

    public int TotalDias { get; set; }

    public int TotalNoches { get; set; }

    public int? MinPax { get; set; }

    public int? MaxPax { get; set; }

    public string? PoliticaCancelacion { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]

    public virtual TblSalidum? IdSalidaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<TblCotizacion>? TblCotizacions { get; set; } = new List<TblCotizacion>();

    [JsonIgnore]
    public virtual ICollection<TblDestinoPaquete>? TblDestinoPaquetes { get; set; } = new List<TblDestinoPaquete>();

    [JsonIgnore]
    public virtual ICollection<TblPaqueteIncluye>? TblPaqueteIncluyes { get; set; } = new List<TblPaqueteIncluye>();

    [JsonIgnore]
    public virtual ICollection<TblPaqueteItinerario>? TblPaqueteItinerarios { get; set; } = new List<TblPaqueteItinerario>();

    [JsonIgnore]
    public virtual ICollection<TblPaqueteNoIncluye>? TblPaqueteNoIncluyes { get; set; } = new List<TblPaqueteNoIncluye>();

    [JsonIgnore]
    public virtual ICollection<TblReserva>? TblReservas { get; set; } = new List<TblReserva>();
}
