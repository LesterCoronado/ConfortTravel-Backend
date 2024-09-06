using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public ReservaController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<ReservaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = this.context.TblReservas
                    .Include(i => i.IdPaqueteViajeNavigation)
                    .Include(i => i.IdVehiculoNavigation)
                    .Select(i=>
                    new
                    {
                        idReserva = i.IdReserva,
                        idPaqueteViaje = i.IdPaqueteViaje,
                        nombrePaqueteViaje = i.IdPaqueteViajeNavigation.Titulo,
                        modalidadPaquete = i.IdPaqueteViajeNavigation.ModalidadPaquete,
                        idVehiculo = i.IdVehiculo,
                        marcaVehiculo = i.IdVehiculoNavigation.Marca,
                        placaVehiculo = i.IdVehiculoNavigation.Placa,
                        fechaSalida = i.FechaSalida,
                        horaSalida = i.HoraSalida,
                        fechaRetorno = i.FechaRetorno,
                        horaRetorno = i.HoraRetorno,
                        totalDias = i.TotalDias,
                        Observaciones = i.Observaciones,
                        Estado = i.Estado
                    }).ToList();

                return Ok(data);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // GET api/<ReservaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var data = this.context.TblReservas
                    .Include(i => i.IdPaqueteViajeNavigation)
                    .Include(i => i.IdVehiculoNavigation)
                    .Where(i => i.IdReserva == id)
                    .Select(i =>
                    new
                    {
                        idReserva = i.IdReserva,
                        idPaqueteViaje = i.IdPaqueteViaje,
                        nombrePaqueteViaje = i.IdPaqueteViajeNavigation.Titulo,
                        modalidadPaquete = i.IdPaqueteViajeNavigation.ModalidadPaquete,
                        idVehiculo = i.IdVehiculo,
                        marcaVehiculo = i.IdVehiculoNavigation.Marca,
                        placaVehiculo = i.IdVehiculoNavigation.Placa,
                        fechaSalida = i.FechaSalida,
                        horaSalida = i.HoraSalida,
                        fechaRetorno = i.FechaRetorno,
                        horaRetorno = i.HoraRetorno,
                        totalDias = i.TotalDias,
                        Observaciones = i.Observaciones,
                        Estado = i.Estado
                    }).FirstOrDefault();

                return Ok(data);
            }

            catch (Exception ex)
            {

                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }

        // POST api/<ReservaController>
        [HttpPost]
        public ActionResult Post([FromBody] TblReserva reserva)
        {
            try
            {
                // Comprobar si el vehículo está disponible en las fechas seleccionadas
                var vehiculoOcupado = context.TblReservas
                    .Where(r => r.IdVehiculo == reserva.IdVehiculo)
                    .Any(r => (reserva.FechaSalida <= r.FechaRetorno && reserva.FechaRetorno >= r.FechaSalida));

                if (vehiculoOcupado)
                {
                    return new BadRequestObjectResult("El vehículo no está disponible en las fechas seleccionadas");
                }
                int? maxId = context.TblReservas.Max(p => (int?)p.IdReserva);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                reserva.IdReserva = nuevoId;
                reserva.TotalDias = (reserva.FechaRetorno - reserva.FechaSalida).Days + 1;
                reserva.Estado = true;


                context.TblReservas.Add(reserva);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<ReservaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblReserva reserva)
        {
            if (reserva.IdReserva == id)
            {
                reserva.TotalDias = (reserva.FechaRetorno - reserva.FechaSalida).Days + 1;
                context.Entry(reserva).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }


        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = context.TblReservas.FirstOrDefault(data => data.IdReserva == id);
                if (data != null)
                {
                    context.TblReservas.Remove(data);
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
