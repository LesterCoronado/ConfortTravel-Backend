using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models
{
    public class TblSalidaDestino
    {
        public int IdSalidaDestino { get; set; }

        public int IdDestino { get; set; }

        public int IdSalida { get; set; }

        public bool Estado { get; set; }

        [JsonIgnore]
        public virtual TblDestino? IdDestinoNavigation { get; set; } = null!;

        [JsonIgnore]
        public virtual TblSalida? IdSalidaNavigation { get; set; } = null!;

        public virtual ICollection<TblCotizacion> TblCotizacions { get; set; } = new List<TblCotizacion>();


    }
}
