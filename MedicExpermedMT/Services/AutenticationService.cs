using MedicExpermedMT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Data;
using static MedicExpermedMT.Services.AppointmentService;

namespace MedicExpermedMT.Services
{
    public class AutenticationService
    {
        private readonly medicossystembdIIIContext _context; // Asume que tienes un DbContext configurado
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AppointmentService> _logger;

        public AutenticationService(medicossystembdIIIContext context, IHttpContextAccessor httpContextAccessor, ILogger<AppointmentService> logger)
        {

            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// Vlidacion de usuario
        /// </summary>
        /// <param name="login"></param>
        /// <param name="clave"></param>
        /// <param name="direccionIp"></param>
        /// <returns></returns>
        public async Task<UserData> ValidarLoginAsync(string login, string clave, string direccionIp = null)
        {
            UserData response = null;

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ValidarLogin", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@direccion_ip", string.IsNullOrEmpty(direccionIp) ? DBNull.Value : (object)direccionIp);

                    await connection.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response = new UserData
                            {
                                IdUsuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                                NombresUsuario = reader.GetString(reader.GetOrdinal("nombres_usuario")),
                                ApellidosUsuario = reader.GetString(reader.GetOrdinal("apellidos_usuario")),
                                EmailUsuario = reader.GetString(reader.GetOrdinal("email_usuario")),
                                PerfilId = reader.GetInt32(reader.GetOrdinal("perfil_id")),
                                NombrePerfil = reader.GetString(reader.GetOrdinal("nombre_perfil")),
                                DescripcionEstablecimiento = reader.IsDBNull(reader.GetOrdinal("descripcion_establecimiento"))
                                    ? null : reader.GetString(reader.GetOrdinal("descripcion_establecimiento")),
                                DireccionUsuario = reader.GetString(reader.GetOrdinal("direccion_usuario")),
                                EspecialidadUsuario = reader.GetString(reader.GetOrdinal("nombre_especialidad")),
                                DireccionEstablecimiento = reader.IsDBNull(reader.GetOrdinal("direccion_establecimiento"))
                                    ? null : reader.GetString(reader.GetOrdinal("direccion_establecimiento")),
                                TokenSesion = reader.GetString(reader.GetOrdinal("token_sesion")),
                                Mensaje = "Login exitoso"
                            };
                        }
                        else
                        {
                            response = new UserData
                            {
                                Mensaje = "Error: Usuario o contraseña incorrectos."
                            };
                        }
                    }
                }
            }

            return response;
        }

       

    }
}
