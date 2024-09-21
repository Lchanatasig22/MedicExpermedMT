using MedicExpermedMT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

namespace MedicExpermedMT.Services
{
    public class ConsultationService
    {
        private readonly medicossystembdIIIContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PatientService> _logger;

        public ConsultationService(medicossystembdIIIContext context, IHttpContextAccessor httpContextAccessor, ILogger<PatientService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }


        /// <summary>
        /// Listar con paginacion
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeRelations"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PaginatedList<Consultum>> GetAllConsultasAsync(int pageNumber = 1, int pageSize = 10, bool includeRelations = true)
        {
            // Obtener el nombre de usuario de la sesión
            var loginUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(loginUsuario))
            {
                throw new Exception("El nombre de usuario no está disponible en la sesión.");
            }

            // Construir la consulta filtrando por el usuario de creación
            IQueryable<Consultum> query = _context.Consulta
                                                  .Where(c => c.UsuariocreacionConsulta == loginUsuario)
                                                  .OrderBy(c => c.FechacreacionConsulta);

            // Si se requiere, incluir las relaciones
            if (includeRelations)
            {
                query = query
                        .Include(c => c.ConsultaDiagnosticos)
                        .Include(c => c.ConsultaImagens)
                        .Include(c => c.ConsultaLaboratorios)
                        .Include(c => c.ConsultaMedicamentos)
                        .Include(c => c.AntecedentesFamiliares)
                        .Include(c => c.ExamenFisicos)
                        .Include(c => c.OrganosSistemas)
                        .Include(c => c.PacienteConsultaPNavigation);
            }

            // Obtener el total de consultas que coinciden con los filtros
            var count = await query.CountAsync();

            // Ejecutar la consulta con paginación
            var items = await query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            // Verificar los datos obtenidos (opcional)
            foreach (var item in items)
            {
                var fecha = item.FechacreacionConsulta?.ToShortDateString() ?? "Fecha no disponible";
                var paciente = item.PacienteConsultaPNavigation?.PrimernombrePacientes ?? "Paciente no disponible";
                Console.WriteLine($"Consulta: {fecha}, Paciente: {paciente}");
            }

            // Retornar la lista paginada de consultas
            return new PaginatedList<Consultum>(items, count, pageNumber, pageSize);
        }

        //insertar consulta
        // Método para insertar la consulta completa
        public async Task<int> InsertarConsultaCompleta(ConsultaRequest consulta)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                using (var command = new SqlCommand("dbo.InsertarConsultaCompleta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de la tabla consulta
                    command.Parameters.AddWithValue("@fechacreacion_consulta", consulta.FechacreacionConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@usuariocreacion_consulta", consulta.UsuarioCreacionConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@historial_consulta", consulta.HistorialConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@secuencial_consulta", consulta.SecuencialConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@paciente_consulta_p", consulta.PacienteConsultaP);
                    command.Parameters.AddWithValue("@motivo_consulta", consulta.MotivoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@enfermedad_consulta", consulta.EnfermedadConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombrepariente_consulta", consulta.NombreParienteConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@signosalarma_consulta", consulta.SignosAlarmaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reconofarmacologicas", consulta.ReconocimientoFarmacologicas ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@tipopariente_consulta", consulta.TipoParienteConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefono_pariente_consulta", consulta.TelefonoParienteConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@temperatura_consulta", consulta.TemperaturaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", consulta.FrecuenciaRespiratoriaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@presionarterialsistolica_consulta", consulta.PresionArterialSistolicaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", consulta.PresionArterialDiastolicaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pulso_consulta", consulta.PulsoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@peso_consulta", consulta.PesoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@talla_consulta", consulta.TallaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@plantratamiento_consulta", consulta.PlanTratamientoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@observacion_consulta", consulta.ObservacionConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@antecedentespersonales_consulta", consulta.AntecedentesPersonalesConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diasincapacidad_consulta", consulta.DiasIncapacidadConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medico_consulta_d", consulta.MedicoConsultaD);
                    command.Parameters.AddWithValue("@especialidad_id", consulta.EspecialidadId);
                    command.Parameters.AddWithValue("@estado_consulta_c", consulta.EstadoConsultaC);

                    // Parámetros JSON para tablas relacionadas
                    command.Parameters.AddWithValue("@alergias", consulta.AlergiasJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@cirugias", consulta.CirugiasJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medicamentos", consulta.MedicamentosJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@laboratorio", consulta.LaboratorioJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@imagenes", consulta.ImagenesJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnosticos", consulta.DiagnosticosJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@organos_sistemas", consulta.OrganosSistemasJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@examen_fisico", consulta.ExamenFisicoJson ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@antecedentes_familiares", consulta.AntecedentesFamiliaresJson ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync(); // O usa ExecuteScalar si necesitas el ID
                }
            }
        }


        public async Task<IEnumerable<Paciente>> BuscarPacientesAsync(int? cedula, string primerNombre, string primerApellido)
        {
            var query = _context.Pacientes.AsQueryable();

            if (cedula.HasValue)
            {
                query = query.Where(p => p.CiPacientes == cedula.Value);
            }

            if (!string.IsNullOrEmpty(primerNombre))
            {
                query = query.Where(p => p.PrimernombrePacientes.Contains(primerNombre));
            }

            if (!string.IsNullOrEmpty(primerApellido))
            {
                query = query.Where(p => p.PrimerapellidoPacientes.Contains(primerApellido));
            }

            // Aquí combinamos las condiciones usando lógica OR
            query = _context.Pacientes.Where(p =>
                (cedula.HasValue && p.CiPacientes == cedula.Value) ||
                (!string.IsNullOrEmpty(primerNombre) && p.PrimernombrePacientes.Contains(primerNombre)) ||
                (!string.IsNullOrEmpty(primerApellido) && p.PrimerapellidoPacientes.Contains(primerApellido))
            );

            return await query.ToListAsync();
        }



    }
}
