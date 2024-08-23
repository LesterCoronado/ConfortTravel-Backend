using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public PaqueteController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<PaqueteController>
       
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var Paquete = this.context.TblPaqueteViajes
                    .Include(i => i.IdSalidaNavigation)
                    .Where(i => i.Estado == true)
                    .Select(
                    i =>
                    new
                    {
                        idPaquete = i.IdPaqueteViaje,
                        titulo = i.Titulo,
                        portada = i.Portada,
                        descripcion = i.Descripcion,
                        modalidadPaquete = i.ModalidadPaquete,
                        idSalida = i.IdSalida,
                        salida = i.IdSalidaNavigation.Direccion,
                        totalDias = i.TotalDias,
                        totalNoches = i.TotalNoches,
                        minPax = i.MinPax,
                        maxPax = i.MaxPax,
                        politicaCancelacion = i.PoliticaCancelacion,
                        estado = i.Estado

                    }

                    ).ToList();

                return Ok(Paquete);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }

        [HttpGet("admin")]
        public IActionResult GetAdmin()
        {
            try
            {
                var Paquete = this.context.TblPaqueteViajes
                    .Include(i => i.IdSalidaNavigation)
                    .Select(
                    i =>
                    new
                    {
                        idPaquete = i.IdPaqueteViaje,
                        titulo = i.Titulo,
                        portada = i.Portada,
                        descripcion = i.Descripcion,
                        modalidadPaquete = i.ModalidadPaquete,
                        idSalida = i.IdSalida,
                        salida = i.IdSalidaNavigation.Direccion,
                        totalDias = i.TotalDias,
                        totalNoches = i.TotalNoches,
                        minPax = i.MinPax,
                        maxPax = i.MaxPax,
                        politicaCancelacion = i.PoliticaCancelacion,
                        estado = i.Estado

                    }

                    ).ToList();

                return Ok(Paquete);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }

        // GET api/<PaqueteController>/5
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var Paquete = this.context.TblPaqueteViajes
                    .Include(i => i.IdSalidaNavigation)
                    .Where(i => i.IdPaqueteViaje == id)
                    .Select(
                    i =>
                    new
                    {
                        idPaquete = i.IdPaqueteViaje,
                        titulo = i.Titulo,
                        portada = i.Portada,
                        descripcion = i.Descripcion,
                        modalidadPaquete = i.ModalidadPaquete,
                        idSalida = i.IdSalida,
                        salida = i.IdSalidaNavigation.Direccion,
                        totalDias = i.TotalDias,
                        totalNoches = i.TotalNoches,
                        minPax = i.MinPax,
                        maxPax = i.MaxPax,
                        politicaCancelacion = i.PoliticaCancelacion,
                        estado = i.Estado

                    }

                    ).ToList();

                return Ok(Paquete);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // POST api/<PaqueteController>
        [HttpPost]
        public ActionResult Post([FromBody] TblPaqueteViaje paquete)

        {
            try
            {
                // Verificar si el correo ya existe en la tabla TblPersona
                bool Existe = context.TblPaqueteViajes.Any(p => p.Titulo == paquete.Titulo);
                if (Existe)
                {
                    return new BadRequestObjectResult("Ya se ha registrado este nombre de paquete");
                }
                int? maxId = context.TblPaqueteViajes.Max(p => (int?)p.IdPaqueteViaje);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                paquete.IdPaqueteViaje = nuevoId;
                paquete.Estado = true;

                context.TblPaqueteViajes.Add(paquete);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PaqueteController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblPaqueteViaje paquete)
        {
            try
            {
                if (paquete.IdPaqueteViaje == id)
                {
                    context.Entry(paquete).State = EntityState.Modified;
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


        // DELETE api/<PaqueteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var Paquete = context.TblPaqueteViajes.FirstOrDefault(data => data.IdPaqueteViaje == id);
                if (Paquete != null)
                {
                    context.TblPaqueteViajes.Remove(Paquete);
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
