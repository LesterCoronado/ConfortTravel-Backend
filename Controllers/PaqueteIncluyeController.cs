using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteIncluyeController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public PaqueteIncluyeController(ConfortTravelContext context)
        {
            this.context = context;
        }
        [HttpGet("editar/{id}")]
        public ActionResult GetByIdPaqueteIncluye(int id)
        {
            try
            {
                var Incluye = this.context.TblPaqueteIncluyes
                   .Where(i => i.IdPaqueteIncluye == id)
                   .ToList();
                return Ok(Incluye);
            }
            catch (Exception ex)
            {
                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // GET api/<PaqueteIncluyeController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var Incluye = this.context.TblPaqueteIncluyes
                   .Where(i => i.IdPaqueteViaje == id)
                   .ToList();
                return Ok(Incluye);
            }
            catch (Exception ex)
            {
                {
                    return StatusCode(500, "Ocurrió un error al procesar la solicitudd");
                }

            }
        }


        // POST api/<PaqueteIncluyeController>
        [HttpPost]
        public ActionResult Post([FromBody] TblPaqueteIncluye incluye)
        {
            try
            {
                bool existe = context.TblPaqueteIncluyes.Any(data => data.Incluye == incluye.Incluye);

                if (existe)
                {
                    return new BadRequestObjectResult("Ya existe un registro que contiene lo mismo");
                }

                int? maxId = context.TblPaqueteIncluyes.Max(data => (int?)data.IdPaqueteIncluye);

                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                incluye.IdPaqueteIncluye = nuevoId;

                incluye.Estado = true;

                // Guardar la nueva cotizacion en la base de datos
                context.TblPaqueteIncluyes.Add(incluye);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblPaqueteIncluye incluye)
        {
            try
            {
                if (incluye.IdPaqueteIncluye == id)
                {
                    incluye.Estado = true;
                    context.Entry(incluye).State = EntityState.Modified;
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

        // DELETE api/<PaqueteIncluyeController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = context.TblPaqueteIncluyes.FirstOrDefault(data => data.IdPaqueteIncluye == id);
                if (data != null)
                {
                    context.TblPaqueteIncluyes.Remove(data);
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
