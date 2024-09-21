using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class AuditoriaLogin
    {
        public int IdAuditoriaLogin { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? FechaLogin { get; set; }
        public bool Exito { get; set; }
        public string? DireccionIp { get; set; }
        public string? Mensaje { get; set; }
    }
}
