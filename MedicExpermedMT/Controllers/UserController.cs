using MedicExpermedMT.Models;
using MedicExpermedMT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicExpermedMT.Controllers
{
    public class UserController : Controller
    {

        private readonly UserService _userService;
        private readonly medicossystembdIIIContext _context;

        public UserController(UserService userService, medicossystembdIIIContext context)
        {
            _userService = userService;
            _context = context;
        }
        [HttpGet("Listar-Usuario")]

        public async Task<IActionResult> ListarUsuarios(int page = 1)
        {
            int pageSize = 10; // Define el número de usuarios por página
            var model = await _userService.GetAllUsersAsync(true, page, pageSize); // Llama al servicio con includeRelations = true

            return View(model); // Retorna el modelo a la vista
        }

        /// <summary>
        /// METODO VISTA CREAR USUARIO
        /// </summary>
        /// <returns></returns>
        [HttpGet("Registrar-Usuario")]
        public IActionResult CrearUsuario()
        {
            try
            {
                ViewBag.Perfiles = _context.Perfils.ToList();
                ViewBag.Establecimientos = _context.Establecimientos.ToList();
                ViewBag.Especialidades = _context.Especialidads.ToList(); // Asegúrate de que estás obteniendo las especialidades correctamente
                return View();
            }
            catch (Exception ex)
            {
                // Manejo de errores, muestra un mensaje de error al usuario o redirige a una página de error
                TempData["ErrorMessage"] = "Ocurrió un problema al cargar los datos. Por favor, inténtalo de nuevo.";
                return RedirectToAction("Error", "Home"); // Redirige a una vista de error o maneja según tu lógica
            }
        }

        /// <summary>
        /// Registro de un nuevo usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Registrar-Usuario")]
        public async Task<IActionResult> CrearUsuario(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Contar los errores
                int errorCount = ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Count();

                // Guardar el mensaje de error en TempData
                TempData["ErrorMessage"] = $"Se encontraron {errorCount} errores de validación. Por favor, revisa los campos marcados.";

                // Opcional: Puedes también registrar los errores específicos si es necesario
                var errorDetails = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage)
                                                     .ToList();
                TempData["ErrorDetails"] = errorDetails;

                return View(model);
            }

            try
            {
                // Mapeamos el ViewModel al modelo de dominio
                var usuario = new UsuarioViewModel
                {
                    CiUsuario = model.CiUsuario,
                    NombresUsuario = model.NombresUsuario,
                    ApellidosUsuario = model.ApellidosUsuario,
                    TelefonoUsuario = model.TelefonoUsuario,
                    EmailUsuario = model.EmailUsuario,
                    DireccionUsuario = model.DireccionUsuario,
                    FirmadigitalUsuario = model.FirmadigitalUsuario,
                    CodigoqrUsuario = model.CodigoqrUsuario,
                    CodigoSenecyt = model.CodigoSenecyt,
                    LoginUsuario = model.LoginUsuario,
                    ClaveUsuario = model.ClaveUsuario,  // Recuerda que se encriptará en el SP
                    CodigoUsuario = model.CodigoUsuario,
                    IntentosFallidos = 0,
                    EstadoUsuario = model.EstadoUsuario,
                    PerfilId = model.PerfilId,
                    EstablecimientoId = model.EstablecimientoId,
                    EspecialidadId = model.EspecialidadId
                };

                // Llamada al servicio para crear el usuario
                await _userService.CreateUserAsync(usuario);

                // Mensaje de éxito
                TempData["SuccessMessage"] = "Usuario creado exitosamente.";
                return RedirectToAction("ListarUsuarios", "User"); // Redirige a una lista o pantalla de usuarios
            }
            catch (InvalidOperationException ex)
            {
                // Mensaje de error
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
            catch (Exception ex)
            {
                // Manejo general de excepciones
                TempData["ErrorMessage"] = "Ocurrió un error inesperado. Inténtalo de nuevo.";
                return View(model);
            }
        }

        /// <summary>
        /// Editar usuario vista
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Editar-Usuario/{id}")]
        public async Task<IActionResult> EditarUsuario(int id)
        {
            var usuario = await _userService.GetUserByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Remover la validación de la contraseña ya que no es necesaria en la carga inicial de la vista
            ModelState.Remove("ClaveUsuario");

            ViewBag.Perfiles = _context.Perfils.ToList();
            ViewBag.Establecimientos = _context.Establecimientos.ToList();
            ViewBag.Especialidades = _context.Especialidads.ToList();

         
            var viewModel = new UsuarioViewModel
            {
                IdUsuario = usuario.IdUsuario,
                CiUsuario = usuario.CiUsuario,
                NombresUsuario = usuario.NombresUsuario,
                ApellidosUsuario = usuario.ApellidosUsuario,
                TelefonoUsuario = usuario.TelefonoUsuario,
                EmailUsuario = usuario.EmailUsuario,
                DireccionUsuario = usuario.DireccionUsuario,
                FirmadigitalUsuario = usuario.FirmadigitalUsuario,
                CodigoqrUsuario = usuario.CodigoqrUsuario,
                CodigoSenecyt = usuario.CodigoSenecyt,
                LoginUsuario = usuario.LoginUsuario,
                CodigoUsuario = usuario.CodigoUsuario,
                IntentosFallidos = usuario.IntentosFallidos,
                EstadoUsuario = usuario.EstadoUsuario,
                PerfilId = usuario.PerfilId.Value, // Asigna el ID del perfil
                EstablecimientoId = usuario.EstablecimientoId,
                EspecialidadId = usuario.EspecialidadId,
            };

            return View(viewModel);
        }

        /// <summary>
        /// Metodo actualizar usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Editar-Usuario/{id}")]
        public async Task<IActionResult> EditarUsuario(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Contar los errores
                int errorCount = ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Count();

                // Guardar el mensaje de error en TempData
                TempData["ErrorMessage"] = $"Se encontraron {errorCount} errores de validación. Por favor, revisa los campos marcados.";

                // Opcional: Puedes también registrar los errores específicos si es necesario
                var errorDetails = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage)
                                                     .ToList();
                TempData["ErrorDetails"] = errorDetails;

                return View(model);
            }

            try
            {
                // Mapeamos el ViewModel al modelo de dominio
                var usuario = new UsuarioViewModel
                {
                    CiUsuario = model.CiUsuario,
                    NombresUsuario = model.NombresUsuario,
                    ApellidosUsuario = model.ApellidosUsuario,
                    TelefonoUsuario = model.TelefonoUsuario,
                    EmailUsuario = model.EmailUsuario,
                    DireccionUsuario = model.DireccionUsuario,
                    FirmadigitalUsuario = model.FirmadigitalUsuario,
                    CodigoqrUsuario = model.CodigoqrUsuario,
                    CodigoSenecyt = model.CodigoSenecyt,
                    LoginUsuario = model.LoginUsuario,
                    ClaveUsuario = model.ClaveUsuario,  // Recuerda que se encriptará en el SP
                    CodigoUsuario = model.CodigoUsuario,
                    IntentosFallidos = 0,
                    EstadoUsuario = model.EstadoUsuario,
                    PerfilId = model.PerfilId,
                    EstablecimientoId = model.EstablecimientoId,
                    EspecialidadId = model.EspecialidadId
                };

                // Llamada al servicio para actualizar el usuario
                var result = await _userService.UpdateUserAsync(usuario);

                // Mensaje de éxito
                TempData["SuccessMessage"] = "Usuario actualizado exitosamente.";
                return RedirectToAction("ListarUsuarios", "User"); // Redirige a una lista o pantalla de usuarios
            }
            catch (InvalidOperationException ex)
            {
                // Mensaje de error
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
            catch (Exception ex)
            {
                // Manejo general de excepciones
                TempData["ErrorMessage"] = "Ocurrió un error inesperado. Inténtalo de nuevo.";
                return View(model);
            }
        }



        /// <summary>
        /// Método para cambiar el estado del usuario
        /// </summary>
        /// <param name="ciUsuario"></param>
        /// <returns></returns>
        // Método para cambiar el estado del usuario
        [HttpPost("CambiarEstadoUsuario/{ciUsuario}")]
        public async Task<IActionResult> CambiarEstadoUsuario(int ciUsuario)
        {
            try
            {
                var result = await _userService.CambiarEstadoUsuarioAsync(ciUsuario);
                TempData["SuccessMessage"] = result;
                return RedirectToAction("ListarUsuarios"); // Redirige a una lista o pantalla de usuarios
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cambiar el estado del usuario: {ex.Message}";
                return RedirectToAction("ListarUsuarios"); // Redirige a una lista o pantalla de usuarios
            }
        }



    }
}
