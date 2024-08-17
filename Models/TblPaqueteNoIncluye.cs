using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models;

public partial class TblPaqueteNoIncluye
{
    public int IdPaqueteNoIncluye { get; set; }

    public int IdPaqueteViaje { get; set; }

    public string NoIncluye { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual TblPaqueteViaje IdPaqueteViajeNavigation { get; set; } = null!;
}
