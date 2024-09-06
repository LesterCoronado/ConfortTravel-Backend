using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelReservaController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public HotelReservaController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<HotelReservaController>
        [HttpGet]
        public ActionResult<TblReservaAlojamiento> Get(int id)
        {
            try
            {
                var data = context.TblReservaAlojamientos
                    .Include(i => i.IdHotelNavigation)
                    .Where(i => i.IdReserva == id)
                    .Select(i =>
                    new
                    {
                        idHotel = i.IdHotel,
                        hotel = i.IdHotelNavigation.Nombre,
                        fechaInicio = i.FechaInicio,
                        fechaFin = i.FechaFin

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


        

        // POST api/<HotelReservaController>
        [HttpPost]
        public ActionResult Post([FromBody] TblReservaAlojamiento ra)
        {
            try
            {
                bool Existe = context.TblReservaAlojamientos.Any(p => p.IdReserva == ra.IdReserva && p.IdHotel == ra.IdHotel);
                if (Existe)
                {
                    return new BadRequestObjectResult("El Hotel ya ha sido asignado a la reserva");
                }

                int? maxId = context.TblReservaAlojamientos.Max(p => (int?)p.IdReservaAlojamiento);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                ra.IdReservaAlojamiento = nuevoId;
                ra.Estado = true;
                ra.TotalDias = (ra.FechaFin - ra.FechaInicio).Days + 1;

                context.TblReservaAlojamientos.Add(ra);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // DELETE api/<HotelReservaController>/5
        [HttpDelete]
        public ActionResult Delete(int idReserva, int idHotel)
        {
            try
            {
                var data = context.TblReservaAlojamientos.FirstOrDefault(data => data.IdReserva == idReserva && data.IdHotel == idHotel);
                if (data != null)
                {
                    context.TblReservaAlojamientos.Remove(data);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("El hotel proporcionado no está asignado a esta reserva");
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
