using Newtonsoft.Json;

namespace MedicExpermedMT.Models
{
    public class UserData
    {
        [JsonProperty("id_usuario")]
        public int IdUsuario { get; set; }

        [JsonProperty("nombres_usuario")]
        public string NombresUsuario { get; set; }

        [JsonProperty("apellidos_usuario")]
        public string ApellidosUsuario { get; set; }

        [JsonProperty("email_usuario")]
        public string EmailUsuario { get; set; }

        [JsonProperty("perfil_id")]
        public int PerfilId { get; set; }

        [JsonProperty("nombre_perfil")]
        public string NombrePerfil { get; set; }

        [JsonProperty("descripcion_establecimiento")]
        public string DescripcionEstablecimiento { get; set; }

        [JsonProperty("direccion_usuario")]
        public string DireccionUsuario { get; set; }

        [JsonProperty("direccion_establecimiento")]
        public string DireccionEstablecimiento { get; set; }

        [JsonProperty("nombre_especialidad")]
        public string EspecialidadUsuario { get; set; }
        [JsonProperty("token_sesion")]
        public string TokenSesion { get; set; }

        public string Mensaje { get; set; }

    }


}
