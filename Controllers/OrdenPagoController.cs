using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenPagoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public OrdenPagoController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<OrdenPagoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var oPago = context.TblOrdenDePagos.ToList();

                return Ok(oPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     
        [HttpPost]
        public ActionResult Post([FromBody] TblOrdenDePago oPago)
        {
            try
            {
      
                // Obtener el valor máximo del ID de la tabla 
                int? maxId = context.TblOrdenDePagos.Max(data => (int?)data.IdOrdenDePago);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                // Asignar el nuevo ID 
                oPago.IdOrdenDePago = nuevoId;
                //con estado true
                oPago.Estado = true;

                oPago.FechaGenerado = DateTime.UtcNow;


                // Guardar en la base de datos
                context.TblOrdenDePagos.Add(oPago);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //recibe como parámetro el id del usuario
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                //Nota: pPendiente = Pago Pendiente => para que los clientes puedan ver el listado de sus pagos pendientes por las cotizaciones realizadas
                var pPendiente = this.context.TblOrdenDePagos
                    .Include(i => i.IdCotizacionNavigation)
                     .Include(i => i.IdCotizacionNavigation.IdPaqueteViajeNavigation)
                    .Where(i => i.IdCotizacionNavigation.IdUsuario == id && i.Estado == true)
                    .Select(
                    i =>
                    new
                    {
                        idOrdenPago = i.IdOrdenDePago,
                        idCotizacion = i.IdCotizacionNavigation.IdCotizacion,
                        idUsuario = i.IdCotizacionNavigation.IdUsuario,
                        idPaqueteViaje = i.IdCotizacionNavigation.IdPaqueteViaje,
                        paqueteViaje = i.IdCotizacionNavigation.IdPaqueteViajeNavigation.Titulo,
                        salida = i.IdCotizacionNavigation.IdPaqueteViajeNavigation.IdSalidaNavigation.Direccion,
                        modalidad = i.IdCotizacionNavigation.IdPaqueteViajeNavigation.ModalidadPaquete,
                        fechaSalida = i.IdCotizacionNavigation.FechaSalida,
                        totalAdultos = i.IdCotizacionNavigation.TotalAdultos,
                        totalNinos = i.IdCotizacionNavigation.TotalNinos,
                        comentario = i.IdCotizacionNavigation.Comentario,
                        precioCotizacion = i.IdCotizacionNavigation.PrecioCotizacion,
                        validoHasta = i.FechaVencimiento,
                        estado = i.Estado,
                        checkout = i.Checkout

                    }

                    ).ToList();

                return Ok(pPendiente);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }

        // DELETE api/<OrdenPagoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
