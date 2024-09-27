using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescuentoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public DescuentoController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<DescuentoController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var descuentos = context.TblDescuentos.ToList();
                return Ok(descuentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }




        // POST api/<DescuentoController>
        [HttpPost]
        public ActionResult Post([FromBody] TblDescuento descuento)
        {
            try
            {
                bool Existe = context.TblDescuentos.Any(data => data.Nombre == descuento.Nombre);

                if (Existe)
                {
                    return new BadRequestObjectResult("Nombre repetido");
                }

                int? maxId = context.TblDescuentos.Max(data => (int?)data.IdDescuento);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;

                // Asignar el nuevo ID a la asignacion que se va a crear
                descuento.IdDescuento = nuevoId;
                descuento.Estado = true;

                // Guardar la nueva cotizacion en la base de datos
                context.TblDescuentos.Add(descuento);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<DescuentoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DescuentoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Buscamos el descuento por id
                var descuento = context.TblDescuentos.FirstOrDefault(data => data.IdDescuento == id);
                if (descuento != null)
                {
                    // Verificamos si el descuento está en la tabla Tbl_Asignardescuento
                    var existeEnAsignardescuento = context.TblDescuentoPlanillas.Any(data => data.IdDescuento == id);

                    if (existeEnAsignardescuento)
                    {
                        // Retornamos un mensaje indicando que el descuento no puede ser eliminado porque está en uso
                        return BadRequest("El descuento está en uso dentro de una planilla");
                    }

                    // Si no está en uso, eliminamos el descuento
                    context.TblDescuentos.Remove(descuento);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("descuento no encontrado.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
