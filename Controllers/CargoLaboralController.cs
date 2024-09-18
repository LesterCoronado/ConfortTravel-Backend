using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoLaboralController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public CargoLaboralController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<CargoLaboralController>
        [HttpGet]
        public ActionResult Get()
        {
            var cargosLaboral= context.TblCargoLaborals.ToList();
            return Ok(cargosLaboral);
        }

        // GET api/<CargoLaboralController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CargoLaboralController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CargoLaboralController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CargoLaboralController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
