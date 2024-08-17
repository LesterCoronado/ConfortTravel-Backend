using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendConfortTravel.DTO;
using BackendConfortTravel.Seguridad;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ConfortTravelContext context;

        public LoginController(ConfortTravelContext context)
        {
            this.context = context;
        }

        // GET: api/<LoginController>
        [HttpGet]
        public IActionResult Get(string email, string password)
        {
            string _password = Encrypt.GetSHA256(password);
            try
            {
                var usuario = this.context.TblAsignarRols
                .Include(i => i.IdUsuarioNavigation.IdPersonaNavigation)
                .Include(i => i.IdRolNavigation)
                .Where(i => i.IdUsuarioNavigation.IdPersonaNavigation.Correo == email
                 && i.IdUsuarioNavigation.Contraseña == _password
                )
                .Select(
                i =>
                new
                {
                    idUsuario = i.IdUsuario,
                    nombre = i.IdUsuarioNavigation.IdPersonaNavigation.NombreCompleto,
                    rol = i.IdRolNavigation.NombreRol,
                    idRol = i.IdRolNavigation.IdRol
                }
                ).FirstOrDefault();

                if (usuario == null)
                {
                    return NotFound("Usuario no enccontrado");

                }

                return Ok(usuario);

            }
            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }





        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<LoginController>
        [HttpPost]
        public ActionResult Post([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                // Verificar si el correo ya existe en la tabla TblPersona
                bool correoExiste = context.TblPersonas.Any(data => data.Correo == usuarioDTO.Correo);

                if (correoExiste)
                {
                    return new BadRequestObjectResult("El usuario ya existe");
                }

                // Obtener el valor máximo del ID de la tabla "persona"
                int? maxId = context.TblPersonas.Max(p => (int?)p.IdPersona);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;


                TblPersona persona = new TblPersona
                {
                    IdPersona = nuevoId,
                    Nombre = usuarioDTO.Nombre,
                    Apellido = usuarioDTO.Apellido,
                    Edad = usuarioDTO.Edad,
                    Direccion = usuarioDTO.Direccion,
                    Telefono = usuarioDTO.Telefono,
                    Sexo = usuarioDTO.Sexo,
                    Correo = usuarioDTO.Correo
                };
                // Guardar la nueva persona en la base de datos
                context.TblPersonas.Add(persona);
                context.SaveChanges();

                // Obtener el valor máximo del ID de la tabla "usuario"
                int? idMax = context.TblUsuarios.Max(p => (int?)p.IdUsuario);

                // Incrementar el ID para el nuevo registro
                int idUsuario = idMax.HasValue ? idMax.Value + 1 : 1;

                // Crear un nuevo usuario relacionado con la persona recién creada
                TblUsuario usuario = new TblUsuario
                {
                    IdUsuario = idUsuario,
                    IdPersona = persona.IdPersona,
                    Contraseña = Encrypt.GetSHA256(usuarioDTO.Contraseña),
                    Estado = true // Estado por defecto
                };

                context.TblUsuarios.Add(usuario);
                context.SaveChanges();

                // Obtener el valor máximo del ID de la tabla "AsignarRol"
                int? idAsignacion = context.TblAsignarRols.Max(p => (int?)p.IdAsignarRol);

                // Incrementar el ID para el nuevo registro
                int _idAsignacion = idAsignacion.HasValue ? idAsignacion.Value + 1 : 1;

                // Crear una nueva asignación de rol para el usuario recién creado
                TblAsignarRol asignarRol = new TblAsignarRol
                {
                    IdAsignarRol = _idAsignacion,
                    IdUsuario = usuario.IdUsuario,
                    IdRol = 2 // ID del rol por defecto (usuario cliente)
                };

                context.TblAsignarRols.Add(asignarRol);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}