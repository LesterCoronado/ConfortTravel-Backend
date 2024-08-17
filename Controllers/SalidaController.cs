using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {

        private readonly ConfortTravelContext context;

        public SalidaController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<SalidaController>
        [HttpGet]
        public IEnumerable<TblSalidum> Get()
        {
            return context.TblSalida.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<TblSalidum> Get(int id)
        {
            try
            {
                var destino = context.TblSalida.Where(i => i.IdSalida == id).FirstOrDefault();
                return Ok(destino);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



        // POST api/<SalidaController>
        [HttpPost]
        public ActionResult Post([FromBody] TblSalidum salida)
        {
            try
            {
                // Verificar si el correo ya existe en la tabla TblPersona
                bool Existe = context.TblSalida.Any(p => p.Direccion == salida.Direccion);
                if (Existe)
                {
                    return new BadRequestObjectResult("La salida ya existe");
                }
                int? maxId = context.TblSalida.Max(p => (int?)p.IdSalida);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                salida.IdSalida = nuevoId;
               

                context.TblSalida.Add(salida);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblSalidum salida)
        {
            if (salida.IdSalida == id)
            {
                
                context.Entry(salida).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<SalidaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var salida = context.TblSalida.FirstOrDefault(data => data.IdSalida == id);
                if (salida != null)
                {
                    context.TblSalida.Remove(salida);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
