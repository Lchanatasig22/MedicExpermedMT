using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class ConsultaMedicamento
    {
        public int IdConsultaMedicamento { get; set; }
        public DateTime? FechacreacionMedicamento { get; set; }
        public int MedicamentoId { get; set; }
        public int ConsultaMedicamentosId { get; set; }
        public string? DosisMedicamento { get; set; }
        public string? ObservacionMedicamento { get; set; }
        public int? SecuencialMedicamento { get; set; }
        public int EstadoMedicamento { get; set; }

        public virtual Consultum ConsultaMedicamentos { get; set; } = null!;
        public virtual Medicamento Medicamento { get; set; } = null!;
    }
}
