using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblPersona
{
    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public int Edad { get; set; }

    public string? Direccion { get; set; }

    public int? Telefono { get; set; }

    public int Sexo { get; set; }

    public string? Correo { get; set; }

    [JsonIgnore]
    public string? NombreCompleto => $"{Nombre} {Apellido}";


    public virtual ICollection<TblItinerarioPasajero> TblItinerarioPasajeros { get; set; } = new List<TblItinerarioPasajero>();

    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();
}
