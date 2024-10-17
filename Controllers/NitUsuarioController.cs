using BackendConfortTravel.Models;
using BackendConfortTravel.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NitUsuarioController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public NitUsuarioController(ConfortTravelContext context)
        {
            this.context = context;
        }


        // Comprueba si un usuario tiene asignado un Nit
        [HttpGet]
        public IActionResult Get(string email)
        {
            try
            {   
                //explain: [nituser = "nit usuario"]
                var nituser = this.context.TblPersonas
                .Where(i => i.Correo == email
                )
                .Select(
                i =>
                new
                {
                    Nombre = i.NombreCompleto,
                    Nit = i.Nit


                }
                ).FirstOrDefault();

                if (nituser == null)
                {
                    return NotFound("Usuario no encontrado");

                }

                return Ok(nituser);

            }
            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // POST api/<NitUsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

       
        // PUT - Actualiza el Nit de un usuario
        [HttpPut("update-nit")]
        public IActionResult UpdateNit(string email, int newNit)
        {
            try
            {
                // Busca al usuario por su correo electrónico
                var usuario = this.context.TblPersonas.FirstOrDefault(u => u.Correo == email);

                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                // Actualiza el campo Nit
                usuario.Nit = newNit;

                // Guarda los cambios en la base de datos
                this.context.SaveChanges();

                return Ok("Nit actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


        // DELETE api/<NitUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
