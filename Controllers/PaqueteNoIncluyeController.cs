using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteNoIncluyeController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public PaqueteNoIncluyeController(ConfortTravelContext context)
        {
            this.context = context;
        }
        [HttpGet("editar/{id}")]
        public ActionResult GetByIdPaqueteNoIncluye(int id)
        {
            try
            {
                var Incluye = this.context.TblPaqueteNoIncluyes
                   .Where(i => i.IdPaqueteNoIncluye == id)
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


        // GET api/<PaqueteNoIncluyeController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var Incluye = this.context.TblPaqueteNoIncluyes
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
        public ActionResult Post([FromBody] TblPaqueteNoIncluye noIncluye)
        {
            try
            {
                bool existe = context.TblPaqueteNoIncluyes.Any(data => data.NoIncluye == noIncluye.NoIncluye);

                if (existe)
                {
                    return new BadRequestObjectResult("Ya existe un registro que contiene lo mismo");
                }

                int? maxId = context.TblPaqueteNoIncluyes.Max(data => (int?)data.IdPaqueteNoIncluye);

                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                noIncluye.IdPaqueteNoIncluye = nuevoId;

                noIncluye.Estado = true;

                // Guardar la nueva cotizacion en la base de datos
                context.TblPaqueteNoIncluyes.Add(noIncluye);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblPaqueteNoIncluye noIncluye)
        {
            try
            {
                if (noIncluye.IdPaqueteNoIncluye == id)

                {
                    noIncluye.Estado = true;
                    context.Entry(noIncluye).State = EntityState.Modified;
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
                var data = context.TblPaqueteNoIncluyes.FirstOrDefault(data => data.IdPaqueteNoIncluye == id);
                if (data != null)
                {
                    context.TblPaqueteNoIncluyes.Remove(data);
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
