using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public CotizacionController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<CotizacionController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var Cotizacion = this.context.TblCotizacions
                    .Include(i => i.IdPaqueteViajeNavigation.IdSalidaNavigation)
                    .Include(i => i.IdUsuarioNavigation.IdPersonaNavigation)
                    .Select(
                    i =>
                    new
                    {
                        idCotizacion = i.IdCotizacion,
                        idUsuario = i.IdUsuario,
                        usuario = i.IdUsuarioNavigation.IdPersonaNavigation.NombreCompleto,
                        idPaqueteViaje = i.IdPaqueteViaje,
                        paqueteViaje = i.IdPaqueteViajeNavigation.Titulo,
                        salida = i.IdPaqueteViajeNavigation.IdSalidaNavigation.Direccion,
                        modalidad = i.IdPaqueteViajeNavigation.ModalidadPaquete,
                        fechaSalida = i.FechaSalida,
                        totalAdultos = i.TotalAdultos,
                        totalNinos = i.TotalNinos,
                        comentario = i.Comentario,
                        precioCotizacion = i.PrecioCotizacion,
                        validoHasta = i.ValidoHasta,
                        estado = i.Estado

                    }

                    ).ToList();

                return Ok(Cotizacion);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // GET api/<CotizacionController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var Cotizacion = this.context.TblCotizacions
                    .Include(i => i.IdPaqueteViajeNavigation.IdSalidaNavigation)
                    .Include(i => i.IdUsuarioNavigation.IdPersonaNavigation)
                    .Where(i => i.IdCotizacion == id)
                    .Select(
                    i =>
                    new
                    {
                        idCotizacion = i.IdCotizacion,
                        idUsuario = i.IdUsuario,
                        usuario = i.IdUsuarioNavigation.IdPersonaNavigation.NombreCompleto,
                        idPaqueteViaje = i.IdPaqueteViaje,
                        paqueteViaje = i.IdPaqueteViajeNavigation.Titulo,
                        salida = i.IdPaqueteViajeNavigation.IdSalidaNavigation.Direccion,
                        modalidad = i.IdPaqueteViajeNavigation.ModalidadPaquete,
                        fechaSalida = i.FechaSalida,
                        totalAdultos = i.TotalAdultos,
                        totalNinos = i.TotalNinos,
                        comentario = i.Comentario,
                        precioCotizacion = i.PrecioCotizacion,
                        validoHasta = i.ValidoHasta,
                        estado = i.Estado
                    }).ToList();
                return Ok(Cotizacion);
            }
            catch (Exception ex)
            {
                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }

        // POST api/<CotizacionController>
        [HttpPost]
        public ActionResult Post([FromBody] TblCotizacion cotizacion)
        {
            try
            {
                bool cotizacionExiste = context.TblCotizacions.Any(data => data.IdCotizacion == cotizacion.IdCotizacion);

                if (cotizacionExiste)
                {
                    return new BadRequestObjectResult("Ya existe una cotizaicon con el mismo ID");
                }

                // Obtener el valor máximo del ID de la tabla "cotizacion"
                int? maxId = context.TblCotizacions.Max(data => (int?)data.IdCotizacion);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                // Asignar el nuevo ID a la cotizacion que se va a crear
                cotizacion.IdCotizacion = nuevoId;

                //Establecer la fecha de validez limite para la actual cotizacion, por defecto será la fecha de salida
                cotizacion.ValidoHasta = cotizacion.FechaSalida;

                //Se establece que la cotizacion se crea con estado True

                cotizacion.Estado = true;

                // Guardar la nueva cotizacion en la base de datos
                context.TblCotizacions.Add(cotizacion);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CotizacionController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblCotizacion cotizacion)
        {
            try
            {
                if (cotizacion.IdCotizacion == id)
                {
                    cotizacion.Estado = true;
                    context.Entry(cotizacion).State = EntityState.Modified;
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



        // DELETE api/<CotizacionController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var Cotizacion = context.TblCotizacions.FirstOrDefault(data => data.IdCotizacion == id);
                if (Cotizacion != null)
                {
                    context.TblCotizacions.Remove(Cotizacion);
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
