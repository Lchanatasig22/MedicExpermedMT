using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class ConsultaImagen
    {
        public int IdConsultaImagen { get; set; }
        public int ImagenId { get; set; }
        public int ConsultaImagenId { get; set; }
        public string? ObservacionImagen { get; set; }
        public int? CantidadImagen { get; set; }
        public int? SecuencialImagen { get; set; }
        public int EstadoImagen { get; set; }

        public virtual Consultum ConsultaImagenNavigation { get; set; } = null!;
        public virtual Imagen Imagen { get; set; } = null!;
    }
}
