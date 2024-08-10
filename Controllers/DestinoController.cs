using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinoController : ControllerBase
    {
        private readonly ConfortContext context;

        public DestinoController(ConfortContext context)
        {
            this.context = context;
        }

        // GET: api/<Destinos>
        [HttpGet]
        public IEnumerable<TblDestino> Get()
        {
            return context.TblDestinos.ToList();
        }

        // GET api/<Destinos>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Destinos>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Destinos>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Destinos>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
