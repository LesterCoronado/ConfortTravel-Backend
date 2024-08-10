using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ConfortContext context;

        public UsuariosController(ConfortContext context)
        {
            this.context = context;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var clientes = await context.TblUsuarios
                .Where(u => u.TblAsignarRols.Any(r => r.IdRolNavigation.IdRol == 2))
                .Include(u => u.IdPersonaNavigation)
                .Select(u => new
                {
                    IdUsuario = u.IdUsuario,
                    nombre = u.IdPersonaNavigation.NombreCompleto,
                    estado = u.Estado

                    // Agrega otros campos que desees incluir aquí
                })
                .ToListAsync();

            return Ok(clientes);
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
