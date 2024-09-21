using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class TokensSesion
    {
        public int UsuarioId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime FechaExpiracion { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
