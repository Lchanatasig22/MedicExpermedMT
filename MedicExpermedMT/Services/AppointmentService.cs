using MedicExpermedMT.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MedicExpermedMT.Services
{
    public class AppointmentService
    {

        private readonly medicossystembdIIIContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(medicossystembdIIIContext context, IHttpContextAccessor httpContextAccessor, ILogger<AppointmentService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        //Obtener todas las citas por estado, medico y ordenadas por fecha
        public AppointmentService(medicossystembdIIIContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Obtener todas las citas
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeRelations"></param>
        /// <returns></returns>


        public async Task<PaginatedList<Citum>> ObtenerCitasAsync(int estado, int pageNumber = 1, int pageSize = 10, bool includeRelations = true)
        {
            var usuarioId = _httpContextAccessor.HttpContext?.Session.GetInt32("UsuarioId");

            // Construye la consulta con los filtros por estado y usuario
            IQueryable<Citum> query = _context.Cita
                                              .Where(c => c.EstadoCita == estado && c.UsuarioId == usuarioId)
                                              .OrderBy(c => c.FechadelacitaCita)
                                              .ThenBy(c => c.HoradelacitaCita);

            // Si se requiere, incluye las relaciones
            if (includeRelations)
            {
                query = query
                        .Include(c => c.Consulta)
                        .Include(c => c.Paciente)
                        .Include(c => c.Usuario);
            }

            // Verifica la consulta antes de ejecutarla
            var queryText = query.ToQueryString();
            Console.WriteLine(queryText);

            // Obtén el total de citas que coinciden con los filtros
            var count = await query.CountAsync();

            // Ejecuta la consulta con paginación para obtener todas las propiedades del modelo Citum
            var items = await query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            // Verifica los datos obtenidos
            foreach (var item in items)
            {
                var fecha = item.FechadelacitaCita?.ToShortDateString() ?? "Fecha no disponible";
                var hora = item.HoradelacitaCita.HasValue ? item.HoradelacitaCita.Value.ToString(@"hh\:mm") : "Hora no disponible";
                var pacienteId = item.PacienteId.ToString();
                var consultaId = item.ConsultaId.HasValue ? item.ConsultaId.ToString() : "Consulta no disponible";

                Console.WriteLine($"Cita: {fecha}, Hora: {hora}, Paciente: {pacienteId}, Consulta: {consultaId}");
            }

            // Retorna la lista paginada de citas con todos los campos
            return new PaginatedList<Citum>(items, count, pageNumber, pageSize);
        }


        /// <summary>
        /// Obtener la cita por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<Citum> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var appointment = await _context.Cita
     .Where(c => c.EstadoCita == 1 && c.IdCita == id)
     .Include(c => c.Paciente)
     .Include(c => c.Usuario)
     .FirstOrDefaultAsync();


                if (appointment == null)
                {
                    throw new KeyNotFoundException("Cita no encontrada.");
                }

                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al obtener la cita por ID.", ex);
            }
        }


        /// <summary>
        /// Obtener horas disponibles
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="medicoId"></param>
        /// <returns></returns>
        public async Task<List<Citum>> ObtenerHorasDisponiblesAsync(int medicoId, DateTime fechaCita)
        {
            var horasDisponibles = new List<Citum>();

            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                using (var command = new SqlCommand("sp_GetHoursAvailable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros para el procedimiento almacenado
                    command.Parameters.AddWithValue("@usuario_id", medicoId);
                    command.Parameters.AddWithValue("@fecha", fechaCita);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            horasDisponibles.Add(new Citum
                            {
                                HoradelacitaCita = reader.GetTimeSpan(reader.GetOrdinal("horadelacitaCita"))
                            });
                        }
                    }
                }
            }

            return horasDisponibles;
        }


        // CREAR CITA SERVICIO

        public async Task<int> CreateAppointmentAsync(Citum appointment)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                using (var command = new SqlCommand("sp_CreateAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@fechacreacion_cita", DateTime.Today);

                    command.Parameters.AddWithValue("@fechadelacita_cita", appointment.FechadelacitaCita);
                    command.Parameters.AddWithValue("@usuariocreacion_cita", appointment.UsuariocreacionCita);
                    command.Parameters.AddWithValue("@horadelacita_cita", appointment.HoradelacitaCita);
                    command.Parameters.AddWithValue("@usuario_id", appointment.UsuarioId);
                    command.Parameters.AddWithValue("@paciente_id", appointment.PacienteId);
                    command.Parameters.AddWithValue("@consulta_id", appointment.ConsultaId.HasValue ? (object)appointment.ConsultaId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@motivo", appointment.Motivo);
                    command.Parameters.AddWithValue("@estado_cita", appointment.EstadoCita);

                    await connection.OpenAsync();

                    // Ejecutar el comando y obtener el ID de la cita
                    var idCita = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(idCita);
                }
            }
        }

        public class AppointmentUpdateException : Exception
        {
            public AppointmentUpdateException(string message) : base(message) { }

            public AppointmentUpdateException(string message, Exception innerException)
                : base(message, innerException) { }
        }


        /// <summary>
        /// ACTUALIZAR CITAS
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AppointmentUpdateException"></exception>
        public async Task<bool> UpdateAppointmentAsync(Citum appointment)
        {
            // Validación previa de los datos
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "La cita no puede ser nula.");
            }

            if (appointment.IdCita <= 0)
            {
                throw new ArgumentException("ID de cita inválido.", nameof(appointment.IdCita));
            }

            try
            {
                using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    using (var command = new SqlCommand("sp_UpdateAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros para la actualización
                        command.Parameters.AddWithValue("@id_cita", appointment.IdCita);
                        command.Parameters.AddWithValue("@fechadelacita_cita", appointment.FechadelacitaCita);
                        command.Parameters.AddWithValue("@horadelacita_cita", appointment.HoradelacitaCita);
                        command.Parameters.AddWithValue("@usuario_id", appointment.UsuarioId);
                        command.Parameters.AddWithValue("@paciente_id", appointment.PacienteId);
                        command.Parameters.AddWithValue("@consulta_id", appointment.ConsultaId.HasValue ? (object)appointment.ConsultaId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@motivo", appointment.Motivo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@estado_cita", appointment.EstadoCita);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected < 0)
                        {
                            return true; // Actualización exitosa
                        }
                        else
                        {
                            throw new AppointmentUpdateException("No se actualizó ninguna fila en la base de datos.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Logging de errores específicos de SQL
                _logger.LogError(sqlEx, "Error SQL al intentar actualizar la cita con ID {AppointmentId}", appointment.IdCita);
                throw new AppointmentUpdateException("Error al actualizar la cita en la base de datos.", sqlEx);
            }
            catch (Exception ex)
            {
                // Logging de errores generales
                _logger.LogError(ex, "Error inesperado al intentar actualizar la cita con ID {AppointmentId}", appointment.IdCita);
                throw new AppointmentUpdateException("Ocurrió un error inesperado al intentar actualizar la cita.", ex);
            }
        }


        /// <summary>
        /// ACTUALIZAR CITA
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AppointmentUpdateException"></exception>
        public async Task<bool> DeleteAppointmentAsync(int appointmentId, int newState)
        {
            if (appointmentId <= 0)
            {
                throw new ArgumentException("ID de cita inválido.", nameof(appointmentId));
            }

            try
            {
                using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    using (var command = new SqlCommand("sp_DeleteAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros necesarios
                        command.Parameters.AddWithValue("@id_cita", appointmentId);
                        command.Parameters.AddWithValue("@estado", newState);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected < 0)
                        {
                            return true; // Actualización exitosa
                        }
                        else
                        {
                            throw new AppointmentUpdateException("No se encontró la cita con el ID especificado o no se pudo actualizar el estado.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Error SQL al intentar eliminar (cambiar estado) la cita con ID {AppointmentId}", appointmentId);
                throw new AppointmentUpdateException("Error al intentar cambiar el estado de la cita en la base de datos.", sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al intentar eliminar (cambiar estado) la cita con ID {AppointmentId}", appointmentId);
                throw new AppointmentUpdateException("Ocurrió un error inesperado al intentar cambiar el estado de la cita.", ex);
            }
        }

    }
}
