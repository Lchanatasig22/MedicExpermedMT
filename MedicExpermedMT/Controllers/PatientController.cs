using MedicExpermedMT.Models;
using MedicExpermedMT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicExpermedMT.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;
        private readonly medicossystembdIIIContext _context;
        private readonly CatalogService _catalogService;

        public PatientController(PatientService patientService, medicossystembdIIIContext context,CatalogService catalogService)
        {
            _patientService = patientService;
            _context = context;
            _catalogService = catalogService;
        }


        /// <summary>
        /// CargarDatosCatalogo
        /// </summary>
        /// <returns></returns>
        private async Task CargarListasDesplegables()
        {
            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentoAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposFormacionAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposSeguroSaludAsync();
        }



        /// <summary>
        /// Mostrar todos los pacientes
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeRelations"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarPacientes(int pageNumber = 1, int pageSize = 10, bool includeRelations = true)
        {
            try
            {
                // Obtiene la lista de pacientes
                var patients = await _patientService.GetAllPatientsAsync(includeRelations, pageNumber, pageSize);

                // Pasa los datos a la vista
                return View(patients);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ViewBag.ErrorMessage = $"Ocurrió un error al obtener los pacientes: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// CREAR UN NUEVO PACIENTE
        /// </summary>
        /// <returns></returns>
     [HttpGet("Creacion-Paciente")]
        public async Task<IActionResult> CrearPaciente()
        {
            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            await CargarListasDesplegables();
         
            return View();
        }

        //metodo crear paciente htppost
        [HttpPost("Creacion-Paciente")]
        public async Task<IActionResult> CrearPaciente(Paciente model)
        {
            if (ModelState.IsValid)
            {
                // Llamamos al método para crear el paciente
                var (newPatientId, message) = await _patientService.CreatePatientAsync(model);

                if (newPatientId.HasValue)
                {
                    // Éxito: Redirigir o mostrar un mensaje
                    TempData["SuccessMessage"] = "Paciente creado exitosamente. ID: " + newPatientId.Value;
                    return RedirectToAction("ListarPacientes", new { id = newPatientId.Value });  // Redirige a la vista de detalles del paciente
                }
                else
                {
                    await CargarListasDesplegables();
                    // Error: Mostrar el mensaje del procedimiento almacenado
                    ModelState.AddModelError("", message ?? "Error al crear el paciente.");
                }
            }

            // Si ModelState no es válido, mostramos los errores en consola para depuración
            foreach (var error in ModelState)
            {
                foreach (var subError in error.Value.Errors)
                {
                    // Registrar los errores específicos
                    Console.WriteLine($"Error en el campo {error.Key}: {subError.ErrorMessage}");
                }
            }

            // Volver a cargar las listas desplegables antes de retornar la vista
            await CargarListasDesplegables();

            // Retornar la vista con los errores de validación mostrados
            return View(model);
        }


        /// <summary>
        /// Actualizar pacientes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpGet("Editar-Paciente/{id}")]
        public async Task<IActionResult> EditarPaciente(int id)
        {
            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            // Obtener el paciente para editar usando el servicio
            var paciente = await _patientService.GetPatientByIdAsync(id);

            if (paciente == null)
            {
                // Si no se encuentra el paciente, redirigir a una página de error o mostrar un mensaje
                return NotFound();
            }

            // Cargar listas desplegables, si es necesario
            await CargarListasDesplegables();

            // Pasar el paciente al modelo de vista
            return View(paciente);
        }

        //POST METODO
        [HttpPost("Editar-Paciente/{id}")]
        public async Task<IActionResult> EditarPaciente(Paciente model)
        {
            if (ModelState.IsValid)
            {
                // Llamamos al método para actualizar el paciente
                var message = await _patientService.UpdatePatientAsync(model);

                if (string.IsNullOrEmpty(message) || message.Contains("actualizado exitosamente"))
                {
                    // Éxito: Redirigir o mostrar un mensaje
                    TempData["SuccessMessage"] = "Paciente actualizado exitosamente.";
                    return RedirectToAction("ListarPacientes"); // Redirige a la lista de pacientes o a la vista de detalles del paciente
                }
                else
                {
                    // Error: Mostrar el mensaje del procedimiento almacenado
                    ModelState.AddModelError("", message);
                }
            }
            else
            {
                // Si ModelState no es válido, mostramos los errores en consola para depuración
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        // Registrar los errores específicos
                        Console.WriteLine($"Error en el campo {error.Key}: {subError.ErrorMessage}");
                    }
                }
            }

            // Volver a cargar las listas desplegables antes de retornar la vista
            await CargarListasDesplegables();

            // Retornar la vista con los errores de validación mostrados
            return View(model);
        }

        /// <summary>
        /// Acción para eliminar un paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeletePatient(int idPaciente)
        {
            string resultMessage = await _patientService.DeletePatientAsync(idPaciente);

            // Devolver un resultado en formato JSON
            return Json(new { resultMessage });
        }

        /// <summary>
        /// Pais
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult BuscarPais(string term)
        {
            var paises = _context.Pais
                .Where(p => p.NombrePais.Contains(term))
                .Select(p => new { id = p.IdPais, nombre = p.NombrePais })  // Devolver ID y nombre
                .ToList();

            return Json(paises);
        }



    }
}
