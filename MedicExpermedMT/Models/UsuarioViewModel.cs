namespace MedicExpermedMT.Models
{
    public class UsuarioViewModel
    {
        
        public int IdUsuario { get; set; }
        public int CiUsuario { get; set; }
        public string NombresUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string DireccionUsuario { get; set; }
        public byte[]? FirmadigitalUsuario { get; set; }  // Asumimos que es un archivo binario
        public byte[]? CodigoqrUsuario { get; set; }      // Asumimos que es un archivo binario
        public string?CodigoSenecyt { get; set; }
        public string LoginUsuario { get; set; }
        public string? ClaveUsuario { get; set; }  // No deberías almacenar la clave en texto plano en el modelo
        public string? CodigoUsuario { get; set; }
        public int? IntentosFallidos { get; set; } = 0;
        public int EstadoUsuario { get; set; }
        public int? PerfilId { get; set; }
        public string? NombrePerfil { get; set; }
        public int? EstablecimientoId { get; set; }
        public string? EstablecimientoNombre { get; set; }

        public int? EspecialidadId { get; set; }
        public string? EspecialidadNombre { get; set; }



    }


}
