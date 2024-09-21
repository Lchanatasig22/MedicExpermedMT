using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class ConsultaDiagnostico
    {
        public int IdConsultaDiagnostico { get; set; }
        public int DiagnosticoId { get; set; }
        public int ConsultaDiagnosticoId { get; set; }
        public string? ObservacionDiagnostico { get; set; }
        public bool? PresuntivoDiagnosticos { get; set; }
        public bool? DefinitivoDiagnosticos { get; set; }
        public int? SecuencialDiagnostico { get; set; }
        public int EstadoDiagnostico { get; set; }

        public virtual Consultum ConsultaDiagnosticoNavigation { get; set; } = null!;
        public virtual Diagnostico Diagnostico { get; set; } = null!;
    }
}
