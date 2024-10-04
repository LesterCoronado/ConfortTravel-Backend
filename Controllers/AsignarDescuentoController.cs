using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignarDescuentoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public AsignarDescuentoController(ConfortTravelContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var descuentos = context.TblDescuentos.ToList();
                return Ok(descuentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // GET api/<descuentoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var descuentos = context.TblDescuentoPlanillas
                .Where(i => i.IdPlanilla == id)
                .Include(i => i.IdDescuentoNavigation)
                .Select(i =>
                new
                {
                    descuento = i.IdDescuentoNavigation.Nombre,
                    Descripción = i.IdDescuentoNavigation.Descripcion,
                    Monto = i.IdDescuentoNavigation.Monto,
                    //FrecuenciaDescuento = i.IdDescuentoNavigation.FrecuenciaDescuento

                })
                .ToList();
                return Ok(descuentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // POST api/<descuentoController>
        [HttpPost]
        public ActionResult Post([FromBody] TblDescuentoPlanilla descuento)
        {
            try
            {
                bool Existe = context.TblDescuentoPlanillas.Any(data => data.IdDescuento == descuento.IdDescuento && data.IdPlanilla == descuento.IdPlanilla);

                if (Existe)
                {
                    return new BadRequestObjectResult("El descuento ya ha sido asignado");
                }

                int? maxId = context.TblDescuentoPlanillas.Max(data => (int?)data.IdDescuentoPlanilla);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                // Asignar el nuevo ID a la asignacion que se va a crear
                descuento.IdDescuentoPlanilla = nuevoId;
                descuento.FechaAsignacion = DateTime.Now;
                descuento.Estado = true;

                // Guardar la nueva cotizacion en la base de datos
                context.TblDescuentoPlanillas.Add(descuento);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // DELETE api/<descuentoController>/5
        [HttpDelete]
        public ActionResult Delete(int idPlanilla, int IdDescuento)
        {
            try
            {
                var descuento = context.TblDescuentoPlanillas.FirstOrDefault(p => p.IdPlanilla == idPlanilla && p.IdDescuento == IdDescuento);
                if (descuento != null)
                {
                    context.TblDescuentoPlanillas.Remove(descuento);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return new BadRequestObjectResult("No se encontró tal asignación");

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
