using MedicExpermedMT.Models;
using MedicExpermedMT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace MedicExpermedMT.Controllers
{
   
    public class AccessController : Controller
    {
        private readonly AutenticationService _autenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccessController(AutenticationService autenticationService, IHttpContextAccessor httpContextAccessor)
        {
            _autenticationService = autenticationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Por favor, completa todos los campos.";
                return View(model);
            }

            // Llamar al servicio de autenticación
            var result = await _autenticationService.ValidarLoginAsync(
                model.LoginUsuario,
                model.ClaveUsuario,
                Request.HttpContext.Connection.RemoteIpAddress?.ToString());

            if (result != null && result.Mensaje == "Login exitoso")
            {
                // Capturar los datos del usuario y almacenarlos en la sesión
                HttpContext.Session.SetString("UsuarioNombre", result.NombresUsuario ?? string.Empty);
                HttpContext.Session.SetString("UsuarioApellido", result.ApellidosUsuario ?? string.Empty);
                HttpContext.Session.SetString("UsuarioEmail", result.EmailUsuario ?? string.Empty);
                HttpContext.Session.SetString("UsuarioEspecialidad", result.EspecialidadUsuario ?? string.Empty);
                HttpContext.Session.SetInt32("PerfilId", result.PerfilId);
                HttpContext.Session.SetInt32("UsuarioId", result.IdUsuario);
                HttpContext.Session.SetString("TokenSesion", result.TokenSesion ?? string.Empty);


                // Redirigir al usuario a la página de inicio o donde corresponda
                return RedirectToAction("Index", "Home");
            }

            // Si la autenticación falla, mostrar un mensaje de error
            ViewBag.ErrorMessage = result?.Mensaje ?? "Error desconocido en la autenticación.";
            return View(model);
        }




        [HttpGet]
        public IActionResult CheckSession()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return Json(false); // La sesión ha expirado
            }
            return Json(true); // La sesión sigue activa
        }
    }
}
