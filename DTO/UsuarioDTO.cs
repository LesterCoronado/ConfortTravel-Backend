namespace BackendConfortTravel.DTO
{
    public class UsuarioDTO
    {
            public int IdPersona { get; set; }

            public string Nombre { get; set; } = null!;

            public string? Apellido { get; set; }

            public int Edad { get; set; }

            public string? Direccion { get; set; }

            public int? Telefono { get; set; }

            public int Sexo { get; set; }

            public string? Correo { get; set; }

            public string Contraseña { get; set; } = null!;

            public DateTime FechaCreacion { get; set; }
    }
}
