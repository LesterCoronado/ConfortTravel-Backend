using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasajeroController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public PasajeroController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<PasajeroController>
        [HttpGet]
        public ActionResult<TblPersona> Get()
        {
            try
            {
                var data = context.TblPersonas.Where(p => !context.TblEmpleados
                                        .Select(e => e.IdPersona)
                                        .Contains(p.IdPersona) &&
                                  !context.TblAsignarRols
                                        .Where(ar => ar.IdRol == 1)
                                        .Select(ar => ar.IdUsuarioNavigation.IdPersona)
                                        .Contains(p.IdPersona)).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost]
        public ActionResult Post([FromBody] TblPersona pasajero)
        {
            try
            {
                bool Existe = context.TblPersonas.Any(p => p.DpiCedula == pasajero.DpiCedula);
                if (Existe)
                {
                    return new BadRequestObjectResult("Pasajero repetido");
                }
                int? maxId = context.TblPersonas.Max(p => (int?)p.IdPersona);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                pasajero.IdPersona = nuevoId;


                context.TblPersonas.Add(pasajero);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
