using System.ComponentModel.DataAnnotations;

namespace MedicExpermedMT.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string ClaveUsuario { get; set; }
    }

}
