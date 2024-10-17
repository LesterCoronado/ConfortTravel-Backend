using System;
using System.Collections.Generic;

namespace BackendConfortTravel.Models
{
    public class FelApiResponse
    {
        public List<RequestData> REQUEST_DATA { get; set; }
        public List<ResponseData> RESPONSE { get; set; }
    }

    public class RequestData
    {
        public int Respuesta { get; set; }
        public string Codigo { get; set; }
        public string Procesador { get; set; }
        public string Mensaje { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class ResponseData
    {
        public string PAIS { get; set; }
        public string NIT { get; set; }
        public string NOMBRE { get; set; }
        public string Direccion { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string MUNICIPIO { get; set; }
    }
}
