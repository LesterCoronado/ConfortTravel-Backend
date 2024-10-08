﻿using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public DestinoController(ConfortTravelContext context)
        {
            this.context = context;
        }

        // GET: api/<Destinos>
        [HttpGet]
        public IEnumerable<TblDestino> Get()
        {
            return context.TblDestinos.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<TblDestino> Get(int id)
        {
            try
            {
                var destino = context.TblDestinos.Where(i => i.IdDestino == id).FirstOrDefault();
                return Ok(destino);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
          
        }


        // POST api/<Destinos>
        [HttpPost]
        public ActionResult Post([FromBody] TblDestino destino)
        {
            try
            {
                // Verificar si el correo ya existe en la tabla TblPersona
                bool Existe = context.TblDestinos.Any(p => p.Nombre == destino.Nombre);
                if (Existe)
                {
                    return new BadRequestObjectResult("El destino ya existe");
                }
                int? maxId = context.TblDestinos.Max(p => (int?)p.IdDestino);
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;
                destino.IdDestino = nuevoId;
                destino.Estado = true;

                context.TblDestinos.Add(destino);
                context.SaveChanges();
                return Ok();

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<Destinos>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblDestino destino)
        {
            if (destino.IdDestino == id)
            {
                destino.Estado = true;
                context.Entry(destino).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<Destinos>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var destino = context.TblDestinos.FirstOrDefault(data => data.IdDestino == id);
                if (destino != null)
                {
                    context.TblDestinos.Remove(destino);
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
