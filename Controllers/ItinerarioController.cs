using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItinerarioController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public ItinerarioController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<ItinerarioController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var Itinerario = this.context.TblPaqueteItinerarios
                    .Include(i => i.IdPaqueteViajeNavigation)
                    .Select(
                    i =>
                    new
                    {
                        idItinerario = i.IdPaqueteItinerario,
                        //idPaqueteViaje = i.IdPaqueteViaje,
                        //paqueteViaje = i.IdPaqueteViajeNavigation.Titulo,
                        actividad = i.Actividad,
                        descripcion = i.Descripcion,
                        horario = i.Horario,
                        estado = i.Estado

                    }

                    ).ToList();

                return Ok(Itinerario);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // GET api/<ItinerarioController>/5
        [HttpGet("{id}")]
        //Se le pasa el IDPAQUETEVIAJE 
        public IActionResult Get(int id)
        {
            try
            {
                var Itinerario = this.context.TblPaqueteItinerarios
                    .Include(i => i.IdPaqueteViajeNavigation)
                    .Where(i => i.IdPaqueteViaje == id)
                    .Select(
                    i =>
                    new
                    {
                        idItinerario = i.IdPaqueteItinerario,
                        //idPaqueteViaje = i.IdPaqueteViaje,
                        //paqueteViaje = i.IdPaqueteViajeNavigation.Titulo,
                        actividad = i.Actividad,
                        descripcion = i.Descripcion,
                        horario = i.Horario,
                        estado = i.Estado

                    }

                    ).ToList();

                return Ok(Itinerario);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }

        // POST api/<ItinerarioController>
        [HttpPost]
        public ActionResult Post([FromBody] TblPaqueteItinerario itinerario)

        {
            try
            {
                // Verificar si el correo ya existe en la tabla TblPersona
                bool Existe = context.TblPaqueteItinerarios.Any(p => p.Actividad == itinerario.Actividad);
                if (Existe)
                {
                    return new BadRequestObjectResult("Ya se ha registrado una actividad con el mismo nombre");
                }
                int? maxId = context.TblPaqueteItinerarios.Max(p => (int?)p.IdPaqueteItinerario);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                itinerario.IdPaqueteItinerario = nuevoId;
                itinerario.Estado = true;

                context.TblPaqueteItinerarios.Add(itinerario);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<ItinerarioController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblPaqueteItinerario itinerario)
        {
            try
            {
                if (itinerario.IdPaqueteItinerario == id)
                {
                    context.Entry(itinerario).State = EntityState.Modified;
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


        // DELETE api/<ItinerarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var Itinerario = context.TblPaqueteItinerarios.FirstOrDefault(data => data.IdPaqueteItinerario == id);
                if (Itinerario != null)
                {
                    context.TblPaqueteItinerarios.Remove(Itinerario);
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
