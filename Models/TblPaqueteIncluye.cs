using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblPaqueteIncluye
{
    public int IdPaqueteIncluye { get; set; }

    public int IdPaqueteViaje { get; set; }

    public string Incluye { get; set; } = null!;

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblPaqueteViaje? IdPaqueteViajeNavigation { get; set; } = null!;
}
