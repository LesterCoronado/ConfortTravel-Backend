using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models;

public partial class TblCargoLaboral
{
    public int IdCargoLaboral { get; set; }

    public int IdDeptoTrabajo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Estado { get; set; }

    [JsonIgnore]
    public virtual TblDeptoTrabajo? IdDeptoTrabajoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<TblPlanilla>? TblPlanillas { get; set; } = new List<TblPlanilla>();
}
