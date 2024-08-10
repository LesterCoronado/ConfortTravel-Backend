namespace BackendConfortTravel.DTO
{
    public class CSD_DTO
    {
        public int IdCotizacion { get; set; }

        public int IdUsuario { get; set; }

        public int IdDestino { get; set; }

        public int IdSalida { get; set; }

        public DateTime FechaSalida { get; set; }

        public DateTime FechaRetorno { get; set; }

        public int TotalDias { get; set; }

        public int TotalAdultos { get; set; }

        public int TotalNinos { get; set; }

        public double? PrecioCotizacion { get; set; }

        public DateTime ValidoHasta { get; set; }

        public bool Estado { get; set; }
    }
}
