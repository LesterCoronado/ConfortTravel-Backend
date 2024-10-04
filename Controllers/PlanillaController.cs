using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanillaController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public PlanillaController(ConfortTravelContext context)
        {
            this.context = context;
        }
        // GET: api/<PlanillaController>
        [HttpGet]
        public IActionResult Get()
        {

            var planilla = context.TblPlanillas
        .Include(p => p.IdEmpleadoNavigation.IdPersonaNavigation)
        .Include(p => p.IdCargoNavigation.IdDeptoTrabajoNavigation) // JOIN con TblCargo
        .Select(p => new
        {
          IdPlanilla =  p.IdPlanilla,
          IdEmpleado = p.IdEmpleado,
          Empleado = p.IdEmpleadoNavigation.IdPersonaNavigation.NombreCompleto,
          idCargo = p.IdCargo,
          Cargo = p.IdCargoNavigation.Nombre,
          DeptoTrabajo = p.IdCargoNavigation.IdDeptoTrabajoNavigation.Nombre,
          FechaContratacion = p.FechaContratacion,
          TipoContrato = p.TipoContrato,
          TiempoContrato =  p.TiempoContrato,
          FechaDeBaja= p.FechaDeBaja,
          SalarioBase = p.SalarioBase,
          NoCuenta = p.NoCuenta,
          TipoCuenta = p.TipoCuenta,
          Moneda = p.Moneda,
          Estado = p.Estado
        })
        .ToList();

            return Ok(planilla);
        }


        // GET api/<PlanillaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var planilla = context.TblPlanillas
        .Include(p => p.IdEmpleadoNavigation.IdPersonaNavigation)
        .Include(p => p.IdCargoNavigation.IdDeptoTrabajoNavigation) // JOIN con TblCargo
        .Select(p => new
        {
            IdPlanilla = p.IdPlanilla,
            IdEmpleado = p.IdEmpleado,
            Empleado = p.IdEmpleadoNavigation.IdPersonaNavigation.NombreCompleto,
            idCargo = p.IdCargo,
            Cargo = p.IdCargoNavigation.Nombre,
            DeptoTrabajo = p.IdCargoNavigation.IdDeptoTrabajoNavigation.Nombre,
            FechaContratacion = p.FechaContratacion,
            TipoContrato = p.TipoContrato,
            TiempoContrato = p.TiempoContrato,
            FechaDeBaja = p.FechaDeBaja,
            SalarioBase = p.SalarioBase,
            NoCuenta = p.NoCuenta,
            TipoCuenta = p.TipoCuenta,
            Moneda = p.Moneda,
            Estado = p.Estado
        })
        .Where(i => i.IdPlanilla == id)
        .FirstOrDefault();
            return Ok(planilla);
        }


        // POST api/<PlanillaController>
        [HttpPost]
        public ActionResult Post([FromBody] TblPlanilla planilla)
        {
            try
            {

                bool EmpleadoExiste = context.TblPlanillas.Any(p => p.IdEmpleado == planilla.IdEmpleado);

                if (EmpleadoExiste)
                {
                    return new BadRequestObjectResult("Este empleado ya ha sido agregado a la planilla");
                }

                int nuevoId = context.TblPlanillas.Max(p => (int?)p.IdPlanilla) ?? 0;
                planilla.IdPlanilla = nuevoId + 1;
                // Establecer el estado en función de si 'FechaDeBaja' es nulo o tiene un valor
                if (planilla.FechaDeBaja == null)
                {
                    planilla.Estado = true;  // Estado activo si 'FechaDeBaja' es nulo
                }
                else
                {
                    planilla.Estado = false; // Estado inactivo si 'FechaDeBaja' tiene un valor
                }
                context.TblPlanillas.Add(planilla);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<PlanillaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TblPlanilla planilla)
        {
            if (planilla.IdPlanilla == id)
            {
                context.Entry(planilla).State = EntityState.Modified;
                // Establecer el estado en función de si 'FechaDeBaja' es nulo o tiene un valor
                if (planilla.FechaDeBaja == null)
                {
                    planilla.Estado = true;  // Estado activo si 'FechaDeBaja' es nulo
                }
                else
                {
                    planilla.Estado = false; // Estado inactivo si 'FechaDeBaja' tiene un valor
                }
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        // DELETE api/<PlanillaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var planilla = context.TblPlanillas.FirstOrDefault(p => p.IdPlanilla == id);
            if (planilla != null)
            {
                context.TblPlanillas.Remove(planilla);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
