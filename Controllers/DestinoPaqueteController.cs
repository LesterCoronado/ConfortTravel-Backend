using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinoPaqueteController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public DestinoPaqueteController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<DestinoPaqueteController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
            var destinoPaquete = this.context.TblDestinoPaquetes
                .Include(i => i.IdPaqueteNavigation)
                .Include(i => i.IdDestinoNavigation)
                .Select(
                i =>
                new
                {
                idDestinoPaquete = i.IdDestinoPaquete,
                idPaquete = i.IdPaquete,
                paquete = i.IdPaqueteNavigation.Titulo,
                idDestino = i.IdDestino,
                destino = i.IdDestinoNavigation.Nombre,
                estado = i.Estado,

                }
                
                ).ToList();

                return Ok(destinoPaquete);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        [HttpGet("{id}")]
        //Se le manda el ID del PAQUETE
        public IActionResult Get(int id)
        {
            {
                try
                {


                    var destinoPaquete = this.context.TblDestinoPaquetes
                        .Include(i => i.IdPaqueteNavigation)
                        .Include(i => i.IdDestinoNavigation)
                        .Where(i => i.IdPaquete == id)
                        .Select(
                        i =>
                        new
                        {
                            idDestinoPaquete = i.IdDestinoPaquete,
                            idPaquete = i.IdPaquete,
                            paquete = i.IdPaqueteNavigation.Titulo,
                            idDestino = i.IdDestino,
                            destino = i.IdDestinoNavigation.Nombre,
                            estado = i.Estado,

                        }

                        ).ToList();

                    return Ok(destinoPaquete);
                }

                catch (Exception ex)
                {

                    {
                        return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                    }

                }

            }
        }

        // POST api/<DestinoPaqueteController>
        [HttpPost]
        public ActionResult Post([FromBody] TblDestinoPaquete destinoPaquete)
        {
            try
            {
                // Verificar si el correo ya existe en la tabla TblPersona
                bool Existe = context.TblDestinoPaquetes.Any(p => p.IdDestino == destinoPaquete.IdDestino);
                if (Existe)
                {
                    return new BadRequestObjectResult("El destino ya existe en el paquete");
                }
                int? maxId = context.TblDestinoPaquetes.Max(p => (int?)p.IdDestinoPaquete);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                destinoPaquete.IdDestinoPaquete = nuevoId;
                destinoPaquete.Estado = true;

                context.TblDestinoPaquetes.Add(destinoPaquete);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<DestinoPaqueteController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblDestinoPaquete destinoPaquete)
        {
            if (destinoPaquete.IdDestinoPaquete == id)
            {

                context.Entry(destinoPaquete).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }


        // DELETE api/<DestinoPaqueteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var destinoPaquete = context.TblDestinoPaquetes.FirstOrDefault(data => data.IdDestinoPaquete == id);
                if (destinoPaquete != null)
                {
                    context.TblDestinoPaquetes.Remove(destinoPaquete);
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
