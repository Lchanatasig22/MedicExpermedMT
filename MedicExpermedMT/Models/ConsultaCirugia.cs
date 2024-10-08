﻿using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class ConsultaCirugia
    {
        public int IdConsultaCirugias { get; set; }
        public DateTime? FechacreacionCirugia { get; set; }
        public int? CatalogocirugiaId { get; set; }
        public int ConsultaCirugiasId { get; set; }
        public string? ObservacionCirugia { get; set; }
        public int? SecuencialCirugias { get; set; }
        public int EstadoCirugias { get; set; }

        public virtual Catalogo? Catalogocirugia { get; set; }
        public virtual Consultum ConsultaCirugias { get; set; } = null!;
    }
}
