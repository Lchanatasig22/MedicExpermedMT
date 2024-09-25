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
            UserData response = new UserData();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ValidarLogin", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@direccion_ip", string.IsNullOrEmpty(direccionIp) ? DBNull.Value : (object)direccionIp);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                await reader.ReadAsync();

                                // Verifica si el resultado contiene la columna "Mensaje"
                                if (reader.GetSchemaTable().Rows.Cast<DataRow>().Any(row => (string)row["ColumnName"] == "Mensaje"))
                                {
                                    // Caso de error
                                    response.Mensaje = SafeRead<string>(reader, "Mensaje", "Error desconocido");
                                }
                                else
                                {
                                    // Caso de éxito
                                    response.IdUsuario = SafeRead<int>(reader, "id_usuario");
                                    response.NombresUsuario = SafeRead<string>(reader, "nombres_usuario");
                                    response.ApellidosUsuario = SafeRead<string>(reader, "apellidos_usuario");
                                    response.EmailUsuario = SafeRead<string>(reader, "email_usuario");
                                    response.PerfilId = SafeRead<int>(reader, "perfil_id");
                                    response.NombrePerfil = SafeRead<string>(reader, "nombre_perfil");
                                    response.DescripcionEstablecimiento = SafeRead<string>(reader, "descripcion_establecimiento");
                                    response.DireccionUsuario = SafeRead<string>(reader, "direccion_usuario");
                                    response.EspecialidadUsuario = SafeRead<string>(reader, "nombre_especialidad");
                                    response.DireccionEstablecimiento = SafeRead<string>(reader, "direccion_establecimiento");
                                    response.TokenSesion = SafeRead<string>(reader, "token_sesion");
                                    response.Mensaje = "Login exitoso";
                                }
                            }
                            else
                            {
                                response.Mensaje = "No se recibió respuesta del servidor.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Aquí deberías usar un sistema de logging apropiado en lugar de Console.WriteLine
                        Console.WriteLine($"Error al ejecutar ValidarLoginAsync: {ex.Message}");
                        response.Mensaje = "Error al procesar la solicitud de inicio de sesión.";
                    }
                }
            }
            return response;
        }

        private T SafeRead<T>(SqlDataReader reader, string columnName, T defaultValue = default)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return !reader.IsDBNull(ordinal) ? (T)reader.GetValue(ordinal) : defaultValue;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"Advertencia: La columna '{columnName}' no está presente en el resultado.");
                return defaultValue;
            }
        }


    }
}
