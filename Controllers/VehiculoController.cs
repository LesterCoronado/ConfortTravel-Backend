using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public VehiculoController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: Para Administracion, trae el listado de vehiculos con estado true y false
        [HttpGet]
        public IActionResult GetVehiculos()
        {
            try
            {
                var data = this.context.TblVehiculos.ToList();

                return Ok(data);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }

        // GET: Para Asignar Vehiculos a Reservas, trae el listado de vehiculos con estado true (que están disponibles para uso)
        [HttpGet("GetVehiculosActivos")]
        public IActionResult GetVehiculosActivos()
        {
            try
            {
                var data = this.context.TblVehiculos.
                    Where(i => i.Estado == true)
                    .ToList();

                return Ok(data);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }



        // GET api/<VehiculoController>/5
        [HttpGet("{id}")]
        public ActionResult<TblVehiculo> Get(int id)
        {
            try
            {
                var data = context.TblVehiculos.Where(i => i.IdVehiculo == id).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        // POST api/<VehiculoController>
        [HttpPost]
        public ActionResult Post([FromBody] TblVehiculo vehiculo)
        {
            try
            {
                bool Existe = context.TblVehiculos.Any(p => p.Placa == vehiculo.Placa);
                if (Existe)
                {
                    return new BadRequestObjectResult("Placa repetida");
                }
                int? maxId = context.TblVehiculos.Max(p => (int?)p.IdVehiculo);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                vehiculo.IdVehiculo = nuevoId;
                

                context.TblVehiculos.Add(vehiculo);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<VehiculoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblVehiculo vehiculo)
        {
            if (vehiculo.IdVehiculo == id)
            {
                context.Entry(vehiculo).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }


        // DELETE api/<VehiculoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = context.TblVehiculos.FirstOrDefault(data => data.IdVehiculo == id);
                if (data != null)
                {
                    context.TblVehiculos.Remove(data);
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
