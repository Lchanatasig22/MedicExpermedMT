using MedicExpermedMT.Models;
using MedicExpermedMT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicExpermedMT.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly AppointmentService _citaService;
        private readonly PatientService _patientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConsultationService _consultationService;
        private readonly CatalogService _catalogService;
        private readonly ILogger<ConsultationController> _logger;
        private readonly medicossystembdIIIContext _context;

        public ConsultationController(AppointmentService citaService, PatientService patientService, IHttpContextAccessor httpContextAccessor, ConsultationService consultationService, CatalogService catalogService, ILogger<ConsultationController> logger, medicossystembdIIIContext medical_SystemContext)
        {
            _citaService = citaService;
            _patientService = patientService;
            _httpContextAccessor = httpContextAccessor;
            _consultationService = consultationService;
            _catalogService = catalogService;
            _logger = logger;
            _context = medical_SystemContext;
        }


        [HttpGet]
        public JsonResult BuscarAlergias(string term)
        {
            var alergias = _context.Catalogos
                .Where(p => p.DescripcionCatalogo.Contains(term) && p.CategoriaCatalogo == "ALERGIAS") // Filtrar por categoría
                .Select(p => new { id = p.IdCatalogo, nombre = p.DescripcionCatalogo })  // Devolver ID y nombre
                .ToList();

            return Json(alergias);
        }


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
            ViewBag.TiposPariente = await _catalogService.ObtenerTiposParentescoAsync();
            ViewBag.TiposAlergias = await _catalogService.ObtenerAlergiasAsync();
            ViewBag.TiposCirugias = await _catalogService.ObtenerCirugiasAsync();
            ViewBag.TiposParienteAntece = await _catalogService.ObtenerAntecedentesFAsync();
            ViewBag.TiposMedicamentos = await _catalogService.ObtenerMedicamentosActivasAsync();
            ViewBag.TiposLaboratorios = await _catalogService.ObtenerLaboratorioActivasAsync();
            ViewBag.TiposImagen = await _catalogService.ObtenerImagenActivasAsync();
            ViewBag.TiposDiagnostico = await _catalogService.ObtenerDiagnosticosActivasAsync();
        }
        /// <summary>
        /// listar consultas
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeRelations"></param>
        /// <returns></returns>
        [HttpGet("Listar-Consultas")]
        public async Task<IActionResult> ListarConsultas(int pageNumber = 1, int pageSize = 10, bool includeRelations = true)
        {
            try
            {
                // Llamar al servicio de consultas con paginación y la opción de incluir relaciones
                var consultas = await _consultationService.GetAllConsultasAsync(pageNumber, pageSize, includeRelations);
                ViewBag.UsuarioEspecialidad = HttpContext.Session.GetString("UsuarioEspecialidad");

                // Pasar las consultas y la información de paginación a la vista
                return View(consultas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new PaginatedList<Consultum>(new List<Consultum>(), 0, pageNumber, pageSize)); // Retorna una lista vacía con la paginación
            }
        }

        //METODO CREAR CONSULTA GET
        [HttpGet("Crear-Consulta")]
        public async Task<IActionResult> CrearConsulta()
        {
            // Asignar datos a ViewBag desde la sesión
            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");
            ViewBag.UsuarioId = HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.UsuarioIdEspecialidad = HttpContext.Session.GetInt32("UsuarioIdEspecialidad");

            // Cargar listas desplegables
            await CargarListasDesplegables();

           

            // Devolver el modelo a la vista
            return View();
        }

        //METODO CREAR CONSULTA POST
        [HttpPost]
        public async Task<IActionResult> CrearConsulta(ConsultaRequest consulta)
        {
            try
            {
                await _consultationService.InsertarConsultaCompleta(consulta);
                TempData["SuccessMessage"] = "Consulta creada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al crear la consulta: {ex.Message}";
            }

            return RedirectToAction("Index"); // Redirige a la vista de índice o a donde necesites
        }

        public IActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(); // Retorna la vista donde mostrarás los mensajes
        }

        [HttpGet("Buscar")]
        public async Task<IActionResult> BuscarPacientes(int? cedula, string primerNombre, string primerApellido)
        {
            if (cedula == null && string.IsNullOrEmpty(primerNombre) && string.IsNullOrEmpty(primerApellido))
            {
                return BadRequest("Debe proporcionar al menos un criterio de búsqueda: cédula, primer nombre o primer apellido.");
            }

            var pacientes = await _consultationService.BuscarPacientesAsync(cedula, primerNombre, primerApellido);

            if (pacientes == null || !pacientes.Any())
            {
                return NotFound("No se encontraron pacientes con los criterios proporcionados.");
            }

            return Ok(pacientes);
        }


    }
}
