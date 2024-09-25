using MedicExpermedMT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MedicExpermedMT.Services
{
    public class UserService
    {
        private readonly medicossystembdIIIContext _context;
        private readonly ILogger<UserService> _logger;
        public UserService(medicossystembdIIIContext context, ILogger<UserService> logger)
        {
            _context = context; _logger = logger;
        }

        /// <summary>
        /// obtener todos los usuarios
        /// </summary>
        /// <param name="includeRelations"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginatedList<Usuario>> GetAllUsersAsync(bool includeRelations = false, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Usuario> query = _context.Usuarios
                                                //.Where(u => u.EstadoUsuario == 1) // Filtra solo los usuarios activos
                                                .OrderBy(u => u.NombresUsuario);   // Ordena por nombre

            if (includeRelations)
            {
                query = query
                        .Include(u => u.Perfil)         // Incluye la relación con Perfil
                        .Include(u => u.Especialidad)   // Incluye la relación con Especialidad
                        .Include(u => u.Establecimiento); // Incluye la relación con Establecimiento
            }

            var count = await query.CountAsync(); // Cuenta los resultados filtrados

            var items = await query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return new PaginatedList<Usuario>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// OBTENER USUARIO POR ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Usuario> GetUserByIdAsync(int id)
        {
            return await _context.Usuarios
                                 .Include(u => u.Perfil)          // Incluye la relación con Perfil
                                 .Include(u => u.Establecimiento) // Incluye la relación con Establecimiento
                                 .Include(u => u.Especialidad)    // Incluye la relación con Especialidad
                                 .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }


        /// <summary>
        /// PERFIL
        /// </summary>
        /// <param name="perfilId"></param>
        /// <returns></returns>
        public async Task<Perfil> GetPerfilByIdAsync(int perfilId)
        {
            return await _context.Perfils
                .SingleOrDefaultAsync(p => p.IdPerfil == perfilId);
        }
        public async Task<Establecimiento> GetEstablecimientoByIdAsync(int establecimientoId)
        {
            return await _context.Establecimientos
                .SingleOrDefaultAsync(e => e.IdEstablecimiento == establecimientoId);
        }
        public async Task<Especialidad> GetEspecialidadByIdAsync(int especilidadId)
        {
            return await _context.Especialidads
                .SingleOrDefaultAsync(e => e.IdEspecialidad == especilidadId);
        }


        /// <summary>
        /// Creacion de nuevo usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CreateUserAsync(UsuarioViewModel usuario)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                var command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_CreateUser";

              
                // Agregar los parámetros esperados por el procedimiento almacenado
                command.Parameters.Add(new SqlParameter("@CiUsuario", SqlDbType.Int) { Value = usuario.CiUsuario });
                command.Parameters.Add(new SqlParameter("@NombresUsuario", SqlDbType.NVarChar, 255) { Value = usuario.NombresUsuario });
                command.Parameters.Add(new SqlParameter("@ApellidosUsuario", SqlDbType.NVarChar, 255) { Value = usuario.ApellidosUsuario });
                command.Parameters.Add(new SqlParameter("@TelefonoUsuario", SqlDbType.NVarChar, 20) { Value = usuario.TelefonoUsuario });
                command.Parameters.Add(new SqlParameter("@EmailUsuario", SqlDbType.NVarChar, 255) { Value = usuario.EmailUsuario });
                command.Parameters.Add(new SqlParameter("@DireccionUsuario", SqlDbType.NVarChar, 1000) { Value = usuario.DireccionUsuario });
                command.Parameters.Add(new SqlParameter("@FirmadigitalUsuario", SqlDbType.VarBinary) { Value = usuario.FirmadigitalUsuario ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@CodigoqrUsuario", SqlDbType.VarBinary) { Value = usuario.CodigoqrUsuario ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@CodigoSenecyt", SqlDbType.NVarChar, 255) { Value = usuario.CodigoSenecyt ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@LoginUsuario", SqlDbType.NVarChar, 255) { Value = usuario.LoginUsuario });
                command.Parameters.Add(new SqlParameter("@ClaveUsuario", SqlDbType.NVarChar, 255) { Value = usuario.ClaveUsuario });
                command.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 20) { Value = usuario.CodigoUsuario ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@IntentosFallidos", SqlDbType.Int) { Value = usuario.IntentosFallidos });
                command.Parameters.Add(new SqlParameter("@EstadoUsuario", SqlDbType.Int) { Value = usuario.EstadoUsuario });
                command.Parameters.Add(new SqlParameter("@PerfilId", SqlDbType.Int) { Value = usuario.PerfilId ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EstablecimientoId", SqlDbType.Int) { Value = usuario.EstablecimientoId ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EspecialidadId", SqlDbType.Int) { Value = usuario.EspecialidadId ?? (object)DBNull.Value });

                await conn.OpenAsync();
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    _logger.LogError("Error ejecutando sp_CreateUser: {ExceptionMessage}", ex.Message);
                    throw new InvalidOperationException("Error al crear el usuario debido a un error en la base de datos.", ex);
                }
            }
        }


        /// <summary>
        /// EDITAR USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<string> UpdateUserAsync(UsuarioViewModel usuario)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@CiUsuario", SqlDbType.Int) { Value = usuario.CiUsuario });
                    command.Parameters.Add(new SqlParameter("@NombresUsuario", SqlDbType.NVarChar, 255) { Value = usuario.NombresUsuario });
                    command.Parameters.Add(new SqlParameter("@ApellidosUsuario", SqlDbType.NVarChar, 255) { Value = usuario.ApellidosUsuario });
                    command.Parameters.Add(new SqlParameter("@TelefonoUsuario", SqlDbType.NVarChar, 20) { Value = usuario.TelefonoUsuario });
                    command.Parameters.Add(new SqlParameter("@EmailUsuario", SqlDbType.NVarChar, 255) { Value = usuario.EmailUsuario });
                    command.Parameters.Add(new SqlParameter("@DireccionUsuario", SqlDbType.NVarChar, 1000) { Value = usuario.DireccionUsuario });

                    // Para los parámetros VARBINARY, asegúrate de pasar datos binarios o DBNull.Value
                    command.Parameters.Add(new SqlParameter("@FirmadigitalUsuario", SqlDbType.VarBinary, -1)
                    {
                        Value = usuario.FirmadigitalUsuario ?? (object)DBNull.Value
                    });
                    command.Parameters.Add(new SqlParameter("@CodigoqrUsuario", SqlDbType.VarBinary, -1)
                    {
                        Value = usuario.CodigoqrUsuario ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new SqlParameter("@CodigoSenecyt", SqlDbType.NVarChar, 255)
                    {
                        Value = usuario.CodigoSenecyt ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new SqlParameter("@LoginUsuario", SqlDbType.NVarChar, 255) { Value = usuario.LoginUsuario });

                    // Contraseña se encriptará en el procedimiento almacenado
                    command.Parameters.Add(new SqlParameter("@ClaveUsuario", SqlDbType.NVarChar, 255)
                    {
                        Value = usuario.ClaveUsuario ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 20)
                    {
                        Value = usuario.CodigoUsuario ?? (object)DBNull.Value
                    });

                    command.Parameters.Add(new SqlParameter("@IntentosFallidos", SqlDbType.Int) { Value = usuario.IntentosFallidos });
                    command.Parameters.Add(new SqlParameter("@EstadoUsuario", SqlDbType.Int) { Value = usuario.EstadoUsuario });

                    command.Parameters.Add(new SqlParameter("@PerfilId", SqlDbType.Int)
                    {
                        Value = usuario.PerfilId ?? (object)DBNull.Value
                    });
                    command.Parameters.Add(new SqlParameter("@EstablecimientoId", SqlDbType.Int)
                    {
                        Value = usuario.EstablecimientoId ?? (object)DBNull.Value
                    });
                    command.Parameters.Add(new SqlParameter("@EspecialidadId", SqlDbType.Int)
                    {
                        Value = usuario.EspecialidadId ?? (object)DBNull.Value
                    });

                    var result = await command.ExecuteScalarAsync();
                    return result?.ToString(); // Usar operador null-coalescing para evitar errores
                }
            }
        }

        /// <summary>
        /// Actualizar usuario estado
        /// </summary>
        /// <param name="ciUsuario"></param>
        /// <returns></returns>
        public async Task<string> CambiarEstadoUsuarioAsync(int ciUsuario)
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_CambiarEstadoUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@CiUsuario", SqlDbType.Int) { Value = ciUsuario });

                    var result = await command.ExecuteScalarAsync();
                    return result?.ToString(); // Usar operador null-coalescing para evitar errores
                }
            }
        }
    }
}
