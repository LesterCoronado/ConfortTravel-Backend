using BackendConfortTravel.DTO;
using BackendConfortTravel.Models;
using BackendConfortTravel.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendConfortTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ConfortTravelContext context;

        public EmpleadoController(ConfortTravelContext context)
        {
            this.context = context;
        }

        // GET: api/<EmpleadoController>
        [HttpGet]
        public IActionResult Get()
        {
            var empleados = context.TblEmpleados
                .Include(i => i.IdPersonaNavigation)
                .Select(
                i =>
                new
                {
                    idEmpleado = i.IdEmpleado,
                    idPersona = i.IdPersona,
                    Nombre = i.IdPersonaNavigation.NombreCompleto,
                    Edad = i.IdPersonaNavigation.Edad,
                    Direccion = i.IdPersonaNavigation.Direccion,
                    Telefono = i.IdPersonaNavigation.Telefono,
                    Sexo = i.IdPersonaNavigation.Sexo,
                    Correo = i.IdPersonaNavigation.Correo,
                    FechaNac = i.FechaNac,
                    EstadoCivil = i.EstadoCivil,
                    FormacionAcademica = i.FormacionAcademica,
                    Dpi = i.Dpi,
                    Altura = i.Altura,
                    Peso = i.Peso,
                    Estado = i.Estado


                }).ToList();

            return Ok(empleados);
        }

        // GET api/<EmpleadoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var empleados = context.TblEmpleados
                .Include(i => i.IdPersonaNavigation)
                .Select(
                i =>
                new
                {
                    idEmpleado = i.IdEmpleado,
                    idPersona = i.IdPersona,
                    Nombre = i.IdPersonaNavigation.Nombre,
                    Apellido = i.IdPersonaNavigation.Apellido,
                    Edad = i.IdPersonaNavigation.Edad,
                    Direccion = i.IdPersonaNavigation.Direccion,
                    Telefono = i.IdPersonaNavigation.Telefono,
                    Sexo = i.IdPersonaNavigation.Sexo,
                    Correo = i.IdPersonaNavigation.Correo,
                    FechaNac = i.FechaNac,
                    EstadoCivil = i.EstadoCivil,
                    FormacionAcademica = i.FormacionAcademica,
                    Dpi = i.Dpi,
                    Altura = i.Altura,
                    Peso = i.Peso,
                    Estado = i.Estado


                })
                .Where(i=> i.idEmpleado == id)
                .FirstOrDefault();

            return Ok(empleados);
        }


        // POST api/<EmpleadoController>
        [HttpPost]
        public ActionResult Post([FromBody] EmpleadoDTO empleadoDTO)
        {
            try
            {
                bool correoExiste = context.TblPersonas.Any(data => data.Correo == empleadoDTO.Correo);

                if (correoExiste)
                {
                    return new BadRequestObjectResult("correo repetido");
                }

                // Obtener el valor máximo del ID de la tabla "persona"
                int? maxId = context.TblPersonas.Max(p => (int?)p.IdPersona);

                // Incrementar el ID para el nuevo registro
                int nuevoId = maxId.HasValue ? maxId.Value + 1 : 1;


                TblPersona persona = new TblPersona
                {
                    IdPersona = nuevoId,
                    Nombre = empleadoDTO.Nombre,
                    Apellido = empleadoDTO.Apellido,
                    Edad = empleadoDTO.Edad,
                    Direccion = empleadoDTO.Direccion,
                    Telefono = empleadoDTO.Telefono,
                    Sexo = empleadoDTO.Sexo,
                    Correo = empleadoDTO.Correo
                };
                // Guardar la nueva persona en la base de datos
                context.TblPersonas.Add(persona);
                context.SaveChanges();

                // Obtener el valor máximo del ID de la tabla "empleado"
                int? idMax = context.TblEmpleados.Max(p => (int?)p.IdEmpleado);

                // Incrementar el ID para el nuevo registro
                int idEmpleado = idMax.HasValue ? idMax.Value + 1 : 1;

                // Crear un nuevo usuario relacionado con la persona recién creada
                TblEmpleado empleado = new TblEmpleado
                {
                    IdEmpleado = idEmpleado,
                    IdPersona = persona.IdPersona,
                    FechaNac = empleadoDTO.FechaNac,
                    EstadoCivil = empleadoDTO.EstadoCivil,
                    FormacionAcademica = empleadoDTO.FormacionAcademica,
                    Dpi = empleadoDTO.Dpi,
                    Altura = empleadoDTO.Altura,
                    Peso = empleadoDTO.Peso,
                    Estado = true // Estado por defecto
                };

                context.TblEmpleados.Add(empleado);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<EmpleadoController>/5
        [HttpPut("{id}")]
        public IActionResult EditEmpleado(int id, [FromBody] EmpleadoDTO data)
        {
            var empleado = context.TblEmpleados
                .Include(e => e.IdPersonaNavigation)
                .FirstOrDefault(e => e.IdEmpleado == id);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }

            // Actualiza los campos editables en TblEmpleado
            empleado.FormacionAcademica = data.FormacionAcademica;
            empleado.IdPersona = data.IdPersona;
            empleado.FechaNac = data.FechaNac;
            empleado.Dpi = data.Dpi;
            empleado.FormacionAcademica = data.FormacionAcademica;
            empleado.EstadoCivil = data.EstadoCivil;
            empleado.Altura = data.Altura;
            empleado.Peso = data.Peso;

            // Actualiza los campos editables en TblPersona
            empleado.IdPersonaNavigation.Nombre = data.Nombre;
            empleado.IdPersonaNavigation.Apellido = data.Apellido;
            empleado.IdPersonaNavigation.Sexo = data.Sexo;
            empleado.IdPersonaNavigation.Telefono = data.Telefono;
            empleado.IdPersonaNavigation.Correo = data.Correo;
            empleado.IdPersonaNavigation.Direccion = data.Direccion;
            empleado.IdPersonaNavigation.Edad = data.Edad;
            context.SaveChanges();
            
            return Ok();
        }


        // DELETE api/<EmpleadoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var personal = context.TblEmpleados.FirstOrDefault(p => p.IdEmpleado == id);
            if (personal != null)
            {
                // Verificar si el empleado está siendo utilizado en la planilla
                var existe = context.TblPlanillas
                    .Where(p => p.IdEmpleado == id)
                    .ToList();

                if (existe.Count > 0)
                {

                    // Construir el mensaje de error
                    var mensajeDeError = "Error, El Empleado está siendo utilizado en la planilla";

                    return NotFound(mensajeDeError);
                }
                else
                {

                    var empleado = context.TblEmpleados.Find(id);
                    if (empleado != null)
                    {
                        context.TblEmpleados.Remove(empleado);
                    }

                    // Guarda los cambios en la base de datos
                    context.SaveChanges();

                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
