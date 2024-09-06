using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasajeroReservaController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public PasajeroReservaController(ConfortTravelContext context)
        {
            this.context = context;
        }


        // GET api/<PasajeroReservaController>/5
        [HttpGet("{id}")]
        //Se le pasa como parámetro del IdReserva para que busque la lista de todos los pasajeros asignados a la reserva en específico
        public ActionResult<TblPasajeroReserva> Get(int id)
        {
            try
            {
                var data = context.TblPasajeroReservas
                    .Include(i => i.IdPersonaNavigation)
                    .Where(i => i.IdReserva == id)
                    .Select(i =>
                    new
                    {
                        idPasajero = i.IdPersona,
                        Pasajero = i.IdPersonaNavigation.NombreCompleto,
                        Nacionalidad = i.IdPersonaNavigation.Nacionalidad,
                        DpiCedula  = i.IdPersonaNavigation.DpiCedula

                    }
                    )
                    .ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        // POST api/<PasajeroReservaController>
        [HttpPost]
        public ActionResult Post([FromBody] TblPasajeroReserva pr)
        {
            try
            {
                bool Existe = context.TblPasajeroReservas.Any(p => p.IdReserva == pr.IdReserva && p.IdPersona == pr.IdPersona);
                if (Existe)
                {
                    return new BadRequestObjectResult("El pasajero ya ha sido asignado a la reserva");
                }

                int? maxId = context.TblPasajeroReservas.Max(p => (int?)p.IdPasajeroReserva);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                pr.IdPasajeroReserva = nuevoId;
                pr.Estado = true;

                context.TblPasajeroReservas.Add(pr);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // DELETE api/<PasajeroReservaController>/5
        [HttpDelete]
        public ActionResult Delete(int idReserva, int idPasajero)
        {
            try
            {
                var data = context.TblPasajeroReservas.FirstOrDefault(data => data.IdReserva == idReserva && data.IdPersona == idPasajero);
                if (data != null)
                {
                    context.TblPasajeroReservas.Remove(data);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("El pasajero proporcionado no está asignado a esta reserva");
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
