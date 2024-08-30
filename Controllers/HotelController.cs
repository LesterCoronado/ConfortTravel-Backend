using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public HotelController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<HotelController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = this.context.TblHotels.ToList();

                return Ok(data);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public ActionResult<TblHotel> Get(int id)
        {
            try
            {
                var data = context.TblHotels.Where(i => i.IdHotel == id).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



        // POST api/<HotelController>
        [HttpPost]
        public ActionResult Post([FromBody] TblHotel hotel)
        {
            try
            {
                bool Existe = context.TblHotels.Any(p => p.Nombre == hotel.Nombre);
                if (Existe)
                {
                    return new BadRequestObjectResult("Hotel repetido");
                }
                int? maxId = context.TblHotels.Max(p => (int?)p.IdHotel);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                hotel.IdHotel = nuevoId;
                hotel.Estado = true;


                context.TblHotels.Add(hotel);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblHotel hotel)
        {
            if (hotel.IdHotel == id)
            {
                context.Entry(hotel).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }


        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = context.TblHotels.FirstOrDefault(data => data.IdHotel == id);
                if (data != null)
                {
                    context.TblHotels.Remove(data);
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
