using System.Text.Json.Serialization;

namespace BackendConfortTravel.Models
{
    public class TblSalida
    {
        public int IdSalida { get; set; }

        public string Direccion { get; set; }
        
        public bool Estado { get; set; }

     [JsonIgnore]
    public virtual ICollection<TblSalidaDestino>? TblSalidaDestinos { get; set; } = new List<TblSalidaDestino>();

    }
}
