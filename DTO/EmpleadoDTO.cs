namespace BackendConfortTravel.DTO
{
    public class EmpleadoDTO
    {
        public int IdPersona { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Apellido { get; set; }

        public int Edad { get; set; }

        public string? Direccion { get; set; }

        public int? Telefono { get; set; }

        public int Sexo { get; set; }

        public string? Correo { get; set; }

        public int IdEmpleado { get; set; }

        public DateTime FechaNac { get; set; }

        public string EstadoCivil { get; set; } = null!;

        public string FormacionAcademica { get; set; } = null!;

        public int Dpi { get; set; }

        public int? Altura { get; set; }

        public int? Peso { get; set; }

        public bool Estado { get; set; }

    }
}
