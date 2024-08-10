using Microsoft.AspNetCore.Mvc;
using BackendConfortTravel.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using BackendConfortTravel.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly ConfortContext context;

        public CotizacionController(ConfortContext context)
        {
            this.context = context;
        }

        // GET: api/<CotizacionController>
        [HttpGet]
        public IActionResult Get()
        {
            var cotizaciones = context.TblCotizacions
                .Include(i => i.IdDestinoNavigation)
                .Include (i => i.IdDestinoNavigation)
                .Include(i => i.IdSalidaDestinoNavigation.IdSalidaNavigation)
                .Include(i => i.IdUsuarioNavigation.IdPersonaNavigation)
                .Select(data => new
                {
                    idCotizacion = data.IdCotizacion,
                    idUsuario = data.IdUsuario, 
                    nombreUsuario = data.IdUsuarioNavigation.IdPersonaNavigation.NombreCompleto,
                    destino = data.IdDestinoNavigation.Nombre,
                    idSalidaDestino = data.IdSalidaDestino,
                    saliendoDesde = data.IdSalidaDestinoNavigation.IdSalidaNavigation.Direccion,
                    fechaSalida = data.FechaSalida,
                    fechaRetorno = data.FechaRetorno,
                    totalDias = data.TotalDias,
                    totalAdultos = data.TotalAdultos,
                    totalNinos = data.TotalNinos,
                    precioCotizacion = data.PrecioCotizacion,
                    validoHasta = data.ValidoHasta,
                    estado = data.Estado

                }).ToList();

            return Ok(cotizaciones);
        }

        // GET api/<CotizacionController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                {
                    var cotizacion = context.TblCotizacions
                        .Include(i => i.IdDestinoNavigation)
                        .Include(i => i.IdUsuarioNavigation.IdPersonaNavigation)
                        .Include(i => i.IdSalidaDestinoNavigation.IdSalidaNavigation)
                        .Where(i => i.IdCotizacion == id)
                        .Select(data => new
                        {
                            idCotizacion = data.IdCotizacion,
                            idUsuario = data.IdUsuario,
                            nombreUsuario = data.IdUsuarioNavigation.IdPersonaNavigation.NombreCompleto,
                            idDestino = data.IdDestino,
                            destino = data.IdDestinoNavigation.Nombre,
                            idSalidaDestino = data.IdSalidaDestino,
                            salida = data.IdSalidaDestinoNavigation.IdSalidaNavigation.Direccion,
                            fechaSalida = data.FechaSalida,
                            fechaRetorno = data.FechaRetorno,
                            totalDias = data.TotalDias,
                            totalAdultos = data.TotalAdultos,
                            totalNinos = data.TotalNinos,
                            precioCotizacion = data.PrecioCotizacion,
                            validoHasta = data.ValidoHasta,
                            estado = data.Estado

                        }).FirstOrDefault();


                    if (cotizacion == null)
                    {
                        return NotFound("No se encontró la cotizacion especificada");


                    }

                    return Ok(cotizacion);
                }

               

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

                // calcula el total de dias
                cotizacion.TotalDias = (cotizacion.FechaRetorno - cotizacion.FechaSalida).Days + 1;

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
        public ActionResult Put(int id, [FromBody] CotizacionDTO cotizacionDTO)
        {
            try
            {
                var cotizacion = context.TblCotizacions.Find(id);
                if (cotizacion == null)
                {
                    return NotFound();
                }

                // Usando reflexión para actualizar los campos del objeto
                var properties = typeof(CotizacionDTO).GetProperties();
                foreach (var property in properties)
                {
                    var newValue = property.GetValue(cotizacionDTO);
                    if (newValue != null)
                    {
                        var targetProperty = typeof(TblCotizacion).GetProperty(property.Name);
                        if (targetProperty != null && targetProperty.CanWrite)
                        {

                            targetProperty.SetValue(cotizacion, newValue);
                        }
                    }
                }
                cotizacion.TotalDias = (cotizacion.FechaRetorno - cotizacion.FechaSalida).Days + 1;

                context.Entry(cotizacion).State = EntityState.Modified;
                    context.SaveChanges();
                    return Ok();
              
  
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
                var cotizacion = context.TblCotizacions.FirstOrDefault(data => data.IdCotizacion == id);
                if (cotizacion != null)
                {
                    context.TblCotizacions.Remove(cotizacion);
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

