using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public BonoController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<BonoController>
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


        

        // POST api/<BonoController>
        [HttpPost]
        public ActionResult Post([FromBody] TblBono bono)
        {
            try
            {
                bool Existe = context.TblBonos.Any(data => data.Nombre == bono.Nombre);

                if (Existe)
                {
                    return new BadRequestObjectResult("Nombre repetido");
                }

                int? maxId = context.TblBonos.Max(data => (int?)data.IdBono);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                // Asignar el nuevo ID a la asignacion que se va a crear
                bono.IdBono = nuevoId;
                bono.Estado = true;

                // Guardar la nueva cotizacion en la base de datos
                context.TblBonos.Add(bono);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<BonoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BonoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Buscamos el bono por id
                var Bono = context.TblBonos.FirstOrDefault(data => data.IdBono == id);
                if (Bono != null)
                {
                    // Verificamos si el bono está en la tabla Tbl_AsignarBono
                    var existeEnAsignarBono = context.TblBonoPlanillas.Any(data => data.IdBono == id);

                    if (existeEnAsignarBono)
                    {
                        // Retornamos un mensaje indicando que el bono no puede ser eliminado porque está en uso
                        return BadRequest("El bono está en uso dentro de una planilla");
                    }

                    // Si no está en uso, eliminamos el bono
                    context.TblBonos.Remove(Bono);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Bono no encontrado.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
