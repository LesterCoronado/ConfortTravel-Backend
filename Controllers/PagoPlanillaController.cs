using BackendConfortTravel.DTO;
using BackendConfortTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoPlanillaController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public PagoPlanillaController(ConfortTravelContext context)
        {
            this.context = context;
        }

        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportCsv([FromQuery] bool incluirBonos = false, [FromQuery] bool incluirDescuentos = false)
        {
            // Obtener la información de la base de datos y mapearla a la clase PlanillaPagoDto
            var planillaData = context.TblPlanillas
                .Where(p => p.Estado == true)  // Filtra por empleados activos
                .Select(p => new PagoPlanillaDto
                {
                    NumeroCuenta = p.NoCuenta,
                    TipoCuenta = p.TipoCuenta,
                    Moneda = p.Moneda,
                    SalarioBase = p.SalarioBase,
                    IdPlanilla = p.IdPlanilla,
                    NombreEmpleado = context.TblEmpleados
                        .Include(e => e.IdPersonaNavigation)
                        .Where(e => e.IdEmpleado == p.IdEmpleado)
                        .Select(e => e.IdPersonaNavigation.NombreCompleto)
                        .FirstOrDefault()
                })
                .ToList();

            // Si se solicita incluir bonos
            if (incluirBonos)
            {
                foreach (var item in planillaData)
                {
                    // Obtener bonos para el empleado
                    var bonos = context.TblBonoPlanillas
                        .Where(bp => bp.IdPlanilla == item.IdPlanilla && bp.Estado == true)
                        .Sum(bp => bp.IdBonoNavigation.Monto);

                    // Sumar los bonos al salario base
                    item.SalarioBase += bonos;
                }
            }

            // Si se solicita incluir descuentos
            if (incluirDescuentos)
            {
                foreach (var item in planillaData)
                {
                    // Obtener descuentos para el empleado
                    var descuentos = context.TblDescuentoPlanillas
                        .Where(dp => dp.IdPlanilla == item.IdPlanilla && dp.Estado == true)
                        .Sum(dp => dp.IdDescuentoNavigation.Monto);

                    // Restar los descuentos al salario base
                    item.SalarioBase -= descuentos;
                }
            }

            // Crear el contenido del CSV
            var csv = new StringBuilder();

            foreach (var item in planillaData)
            {

                csv.AppendLine($"{item.NumeroCuenta},{item.TipoCuenta},{item.Moneda},{item.SalarioBase},{item.NombreEmpleado}");
            }

            // Devolver el CSV como archivo descargable
            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "pago_planilla.csv");
        }




    }
}
