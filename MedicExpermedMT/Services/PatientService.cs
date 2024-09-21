using MedicExpermedMT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MedicExpermedMT.Services
{
    public class PatientService
    {
        private readonly medicossystembdIIIContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PatientService(medicossystembdIIIContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Obtener todas las consultas
        /// </summary>
        /// <param name="includeRelations"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginatedList<Paciente>> GetAllPatientsAsync(bool includeRelations = false, int pageNumber = 1, int pageSize = 10)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            IQueryable<Paciente> query = _context.Pacientes
                                                  .Where(p => p.EstadoPacientes == 1)
                                                  //.Where(p => p.UsuariocreacionPacientes == currentUser)
                                                  .OrderBy(p => p.PrimernombrePacientes);

            if (includeRelations)
            {
                query = query
                        .Include(p => p.TipodocumentoPacientesCaNavigation)
                        .Include(p => p.TiposangrePacientesCaNavigation)
                        .Include(p => p.SegurosaludPacientesCaNavigation)
                        .Include(p => p.EstadocivilPacientesCaNavigation)
                        .Include(p => p.FormacionprofesionalPacientesCaNavigation)
                        .Include(p => p.NacionalidadPacientesPaNavigation)
                        .Include(p => p.ProvinciaPacientesLNavigation)
                        .Include(p => p.SexoPacientesCaNavigation);
            }

            // Verifica la consulta antes de ejecutar
            var queryText = query.ToQueryString();
            Console.WriteLine(queryText);

            var count = await query.CountAsync();

            var items = await query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            // Verifica los datos obtenidos
            foreach (var item in items)
            {
                Console.WriteLine($"Paciente: {item.PrimernombrePacientes}, Nacionalidad: {item.NacionalidadPacientesPaNavigation?.GentilicioPais}");
            }

            return new PaginatedList<Paciente>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Get patient por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelations"></param>
        /// <returns></returns>
        public async Task<Paciente?> GetPatientByIdAsync(int id, bool includeRelations = true)
        {
            IQueryable<Paciente> query = _context.Pacientes
                                                  .Where(p => p.IdPacientes == id);

            if (includeRelations)
            {
                query = query
                        .Include(p => p.TipodocumentoPacientesCaNavigation)
                        .Include(p => p.TiposangrePacientesCaNavigation)
                        .Include(p => p.SegurosaludPacientesCaNavigation)
                        .Include(p => p.EstadocivilPacientesCaNavigation)
                        .Include(p => p.FormacionprofesionalPacientesCaNavigation)
                        .Include(p => p.NacionalidadPacientesPaNavigation)
                        .Include(p => p.ProvinciaPacientesLNavigation)
                        .Include(p => p.SexoPacientesCaNavigation);
            }

            // Ejecutar la consulta y obtener el paciente
            var paciente = await query.SingleOrDefaultAsync();

            // Verifica los datos obtenidos (opcional)
            if (paciente != null)
            {
                Console.WriteLine($"Paciente encontrado: {paciente.PrimernombrePacientes}, Nacionalidad: {paciente.NacionalidadPacientesPaNavigation?.GentilicioPais}");
            }
            else
            {
                Console.WriteLine("Paciente no encontrado.");
            }

            return paciente;
        }


        /// <summary>
        /// Crear un nuevo paciente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public async Task<(int? NewPatientId, string Message)> CreatePatientAsync(Paciente model)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_CreatePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@usuariocreacion_pacientes", model.UsuariocreacionPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@tipodocumento_pacientes_ca", model.TipodocumentoPacientesCa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ci_pacientes", model.CiPacientes);
                    command.Parameters.AddWithValue("@primernombre_pacientes", model.PrimernombrePacientes);
                    command.Parameters.AddWithValue("@segundonombre_pacientes", model.SegundonombrePacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@primerapellido_pacientes", model.PrimerapellidoPacientes);
                    command.Parameters.AddWithValue("@segundoapellido_pacientes", model.SegundoapellidoPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sexo_pacientes_ca", model.SexoPacientesCa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fechanacimiento_pacientes", model.FechanacimientoPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@edad_pacientes", model.EdadPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@tiposangre_pacientes_ca", model.TiposangrePacientesCa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@donante_pacientes", model.DonantePacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estadocivil_pacientes_ca", model.EstadocivilPacientesCa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@formacionprofesional_pacientes_ca", model.FormacionprofesionalPacientesCa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefonofijo_pacientes", model.TelefonofijoPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefonocelular_pacientes", model.TelefonocelularPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email_pacientes", model.EmailPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nacionalidad_pacientes_pa", model.NacionalidadPacientesPa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@provincia_pacientes_l", model.ProvinciaPacientesL ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion_pacientes", model.DireccionPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ocupacion_pacientes", model.OcupacionPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@empresa_pacientes", model.EmpresaPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@segurosalud_pacientes_ca", model.SegurosaludPacientesCa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estado_pacientes", model.EstadoPacientes);

                    // Parámetros de salida
                    var newPatientIdParam = new SqlParameter("@NewPatientId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(newPatientIdParam);

                    var messageParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 4000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(messageParam);

                    // Ejecutar el procedimiento
                    await command.ExecuteNonQueryAsync();

                    // Obtener los resultados y verificar si es DBNull antes de la conversión
                    var newPatientId = newPatientIdParam.Value != DBNull.Value ? (int?)newPatientIdParam.Value : null;
                    var message = messageParam.Value != DBNull.Value ? (string)messageParam.Value : null;

                    return (newPatientId, message);
                }
            }
        }

        /// <summary>
        /// Actualizar Paciente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> UpdatePatientAsync(Paciente model)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_UpdatePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@id_pacientes", model.IdPacientes);
                    command.Parameters.AddWithValue("@usuariomodificacion_pacientes", model.UsuariomodificacionPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@tipodocumento_pacientes_ca", model.TipodocumentoPacientesCa);
                    command.Parameters.AddWithValue("@ci_pacientes", model.CiPacientes);
                    command.Parameters.AddWithValue("@primernombre_pacientes", model.PrimernombrePacientes);
                    command.Parameters.AddWithValue("@segundonombre_pacientes", model.SegundonombrePacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@primerapellido_pacientes", model.PrimerapellidoPacientes);
                    command.Parameters.AddWithValue("@segundoapellido_pacientes", model.SegundoapellidoPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sexo_pacientes_ca", model.SexoPacientesCa);
                    command.Parameters.AddWithValue("@fechanacimiento_pacientes", model.FechanacimientoPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@edad_pacientes", model.EdadPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@tiposangre_pacientes_ca", model.TiposangrePacientesCa);
                    command.Parameters.AddWithValue("@donante_pacientes", model.DonantePacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estadocivil_pacientes_ca", model.EstadocivilPacientesCa);
                    command.Parameters.AddWithValue("@formacionprofesional_pacientes_ca", model.FormacionprofesionalPacientesCa);
                    command.Parameters.AddWithValue("@telefonofijo_pacientes", model.TelefonofijoPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefonocelular_pacientes", model.TelefonocelularPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email_pacientes", model.EmailPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nacionalidad_pacientes_pa", model.NacionalidadPacientesPa);
                    command.Parameters.AddWithValue("@provincia_pacientes_l", model.ProvinciaPacientesL);
                    command.Parameters.AddWithValue("@direccion_pacientes", model.DireccionPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ocupacion_pacientes", model.OcupacionPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@empresa_pacientes", model.EmpresaPacientes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@segurosalud_pacientes_ca", model.SegurosaludPacientesCa);
                    command.Parameters.AddWithValue("@estado_pacientes", model.EstadoPacientes);

                    // Parámetro de salida para el mensaje
                    var messageParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 4000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(messageParam);

                    try
                    {
                        await command.ExecuteNonQueryAsync();

                        // Obtener el mensaje de salida
                        var message = messageParam.Value != DBNull.Value ? (string)messageParam.Value : "Error desconocido.";

                        return message;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        return $"Error: {ex.Message}";
                    }
                }
            }
        }


        /// <summary>
        /// Eliminar paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public async Task<string> DeletePatientAsync(int idPaciente)
        {
            // Define the output parameter
            string resultMessage = "Error al eliminar el paciente.";

            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_DeletePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    command.Parameters.AddWithValue("@IdPaciente", idPaciente);

                    // Add output parameter
                    var outputMessage = new SqlParameter("@ResultMessage", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    try
                    {
                        await command.ExecuteNonQueryAsync();

                        // Get the result message
                        resultMessage = outputMessage.Value.ToString();
                    }
                    catch (SqlException ex)
                    {
                        resultMessage = $"Error: {ex.Message}";
                    }
                }
            }

            return resultMessage;
        }

    }
}
