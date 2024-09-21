using MedicExpermedMT.Models;
using MedicExpermedMT.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedicExpermedMT.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _citaService;
        private readonly PatientService _patientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentController(AppointmentService citaService, PatientService patientService, IHttpContextAccessor httpContextAccessor)
        {
            _citaService = citaService;
            _patientService = patientService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Acción para listar citas
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarCitas(int estado = 1, int pageNumber = 1, int pageSize = 10, bool includeRelations = true)
        {
            var citas = await _citaService.ObtenerCitasAsync(estado, pageNumber, pageSize, includeRelations);
            ViewBag.UsuarioEspecialidad = HttpContext.Session.GetString("UsuarioEspecialidad");

            // Opcional: Pasar información adicional a la vista si es necesario
            ViewBag.Estado = estado;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(citas);
        }

        /// <summary>
        /// Acción para obtener horas disponibles
        /// </summary>
        /// <param name="fechaCita"></param>
        /// <param name="medicoId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ObtenerHorasDisponibles(int medicoId, DateTime fechaCita)
        {
            var horasDisponibles = await _citaService.ObtenerHorasDisponiblesAsync(medicoId, fechaCita);

            // Imprimir las horas disponibles en la consola del servidor
            Console.WriteLine("Horas disponibles obtenidas:");
            foreach (var hora in horasDisponibles)
            {
                Console.WriteLine(hora.HoradelacitaCita); // Asumiendo que `HoradelacitaCita` es el campo que contiene la hora
            }

            return Json(horasDisponibles);
        }


        [HttpGet ("Generar-citas")]
        public async Task<IActionResult> CrearCita(int pacienteId)
        {
            ViewBag.UsuarioId = HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");
            var paciente = await _patientService.GetPatientByIdAsync(pacienteId);

            ViewBag.PacienteId = paciente.IdPacientes; // Asegúrate de asignar solo el ID del paciente.

            return View();
        }

        /// <summary>
        /// Acción para procesar la creación de citas (POST)
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpPost("Nueva-Cita")]
        public async Task<IActionResult> GenerarCita([FromBody] Citum appointment)
        {
            // Verificar si el modelo es null
            if (appointment == null)
            {
                return BadRequest("Invalid appointment data.");
            }

            // Verificar si el modelo es válido según las validaciones del modelo
            if (!ModelState.IsValid)
            {
                // Recopilar los errores de validación en una lista
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            try
            {
                // Llamar al servicio para crear la cita
                int id = await _citaService.CreateAppointmentAsync(appointment);
                return Ok(new { Id = id });
            }
            catch (Exception ex)
            {
                // Logging detallado del error
                Console.WriteLine($"Error al crear la cita: {ex.Message}");
                // Manejar cualquier excepción aquí
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Metodo vista para EditarCita
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Actualizar-Cita")]
        public async Task<IActionResult> ActualizarCita(int id)
        {
            // Obtener los valores de la sesión
            ViewBag.UsuarioId = HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            // Asignar la ID de la cita al ViewBag
            ViewBag.CitaId = id;

            // Obtener la cita desde el servicio por ID
            var cita = await _citaService.GetAppointmentByIdAsync(id);

            // Si la cita no se encuentra, retorna un 404
            if (cita == null)
            {
                return NotFound();
            }

            // Retornar la vista con el modelo de la cita cargado
            return View(cita);
        }

        /// <summary>
        /// Metodo para actualizar la cita
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpPost("Actualizar-Cita")]
        public async Task<IActionResult> ActualizarCita([FromBody] Citum appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Invalid appointment data.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            try
            {
                // Asumiendo que tienes un método de servicio para actualizar la cita
                bool success = await _citaService.UpdateAppointmentAsync(appointment);
                if (success)
                {
                    return Ok(new { Message = "Cita actualizada con éxito" });
                }
                else
                {
                    return StatusCode(500, "No se pudo actualizar la cita.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la cita: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Actualixar estado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        /// <summary>
        /// Actualixar estado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost("Cancelar-Cita")]
        public async Task<IActionResult> CancelarCita(int id, int estado)
        {
            // Llamada al servicio para cancelar la cita
            bool result = await _citaService.DeleteAppointmentAsync(id, estado);

            if (result)
            {
                // Si la cita se cancela con éxito, redirigir a la vista "ListarCitas"
                return RedirectToAction("ListarCitas");
            }
            else
            {
                // En caso de fallo, podrías redirigir a una vista de error o devolver un mensaje
                return View("Error", new { message = "No se pudo cancelar la cita." });
            }
        }

    }
}

