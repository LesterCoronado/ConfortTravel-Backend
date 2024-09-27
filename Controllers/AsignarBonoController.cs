using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignarBonoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public AsignarBonoController(ConfortTravelContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var bonos = context.TblBonos.ToList();
                return Ok(bonos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // GET api/<BonoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var bonos = context.TblBonoPlanillas
                .Where(i => i.IdPlanilla == id)
                .Include(i => i.IdBonoNavigation)
                .Select(i =>
                new
                {
                    Bono = i.IdBonoNavigation.Nombre,
                    Descripción = i.IdBonoNavigation.Descripcion,
                    Monto = i.IdBonoNavigation.Monto,
                    FrecuenciaPago = i.IdBonoNavigation.FrecuenciaPago

                })
                .ToList();
                return Ok(bonos);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }


        }

        // POST api/<BonoController>
        [HttpPost]
        public ActionResult Post([FromBody] TblBonoPlanilla bono)
        {
            try
            {
                bool Existe = context.TblBonoPlanillas.Any(data => data.IdBono == bono.IdBono && data.IdPlanilla == bono.IdPlanilla);

                if (Existe)
                {
                    return new BadRequestObjectResult("El bono ya ha sido asignado");
                }

                int? maxId = context.TblBonoPlanillas.Max(data => (int?)data.IdBonoPlanilla);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                // Asignar el nuevo ID a la asignacion que se va a crear
                bono.IdBonoPlanilla = nuevoId;
                bono.FechaAsignacion = DateTime.Now;
                bono.Estado = true;

                // Guardar la nueva cotizacion en la base de datos
                context.TblBonoPlanillas.Add(bono);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


      

        // DELETE api/<BonoController>/5
        [HttpDelete]
        public ActionResult Delete(int idPlanilla, int idBono)
        {
            try
            {
                var bono = context.TblBonoPlanillas.FirstOrDefault(p => p.IdPlanilla == idPlanilla && p.IdBono == idBono);
                if (bono != null)
                {
                    context.TblBonoPlanillas.Remove(bono);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return new BadRequestObjectResult("No se encontró tal asignación");

                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
