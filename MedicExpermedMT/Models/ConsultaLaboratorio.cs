using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class ConsultaLaboratorio
    {
        public int IdLaboratorioConsulta { get; set; }
        public int? CantidadLaboratorio { get; set; }
        public int ConsultaLaboratorioId { get; set; }
        public string? Observacion { get; set; }
        public int CatalogoLaboratorioId { get; set; }
        public int? SecuencialLaboratorio { get; set; }
        public int EstadoLaboratorio { get; set; }

        public virtual Laboratorio CatalogoLaboratorio { get; set; } = null!;
        public virtual Consultum ConsultaLaboratorioNavigation { get; set; } = null!;
    }
}
