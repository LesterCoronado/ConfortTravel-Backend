using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaDestinoController : ControllerBase
    {
        private readonly ConfortContext context;

        public SalidaDestinoController(ConfortContext context)
        {
            this.context = context;
        }

        // GET: api/<SalidaDestinoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SalidaDestinoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                {
                    var destino = context.TblSalidaDestinos
                        .Include(i => i.IdDestinoNavigation)
                         .Include(i => i.IdSalidaNavigation)
                        .Where(i => i.IdDestino == id)
                        .Select(data => new 
                        {
                            //idDestino = data.IdDestino,
                            //nombre = data.IdDestinoNavigation.Nombre,
                            idSalidaDestino = data.IdSalidaDestino,
                            salida = data.IdSalidaNavigation.Direccion,
                            estado = data.Estado

                        }).ToList();


                    if (destino == null)
                    {
                        return NotFound("No se encontró el destino especificado");


                    }

                    return Ok(destino);
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[HttpGet("hola")]
        //public IActionResult Get(int ids)
        //{
        //    try
        //    {
        //        {
        //            var destino = context.TblSalidaDestinos
        //                .Include(i => i.IdDestinoNavigation)
        //                 .Include(i => i.IdSalidaNavigation)
        //                .Where(i => i.IdDestino == ids)
        //                .Select(data => new
        //                {
        //                    //idDestino = data.IdDestino,
        //                    //nombre = data.IdDestinoNavigation.Nombre,
        //                    idSalida = data.IdSalidaNavigation.IdSalida,
        //                    salida = data.IdSalidaNavigation.Direccion,
        //                    estado = data.Estado

        //                }).ToList();


        //            if (destino == null)
        //            {
        //                return NotFound("No se encontró el destino especificado");


        //            }

        //            return Ok(destino);
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}


        // POST api/<SalidaDestinoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SalidaDestinoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SalidaDestinoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
