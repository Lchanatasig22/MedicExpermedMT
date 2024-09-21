namespace MedicExpermedMT.Models
{
    public class ConsultaRequest
    {
        public AntecedentesFamiliare AntecedentesFamiliares { get; set; } = new AntecedentesFamiliare();
        public OrganosSistema OrganosSistemas { get; set; } = new OrganosSistema();
        public ExamenFisico ExamenFisico { get; set; } = new ExamenFisico();

        // Parámetros de la tabla consulta
        public DateTime? FechacreacionConsulta { get; set; }
        public string UsuarioCreacionConsulta { get; set; }
        public string HistorialConsulta { get; set; }
        public string SecuencialConsulta { get; set; }
        public int PacienteConsultaP { get; set; }
        public string MotivoConsulta { get; set; }
        public string EnfermedadConsulta { get; set; }
        public string NombreParienteConsulta { get; set; }
        public string SignosAlarmaConsulta { get; set; }
        public string ReconocimientoFarmacologicas { get; set; }
        public int? TipoParienteConsulta { get; set; }
        public string TelefonoParienteConsulta { get; set; }
        public string TemperaturaConsulta { get; set; }
        public string FrecuenciaRespiratoriaConsulta { get; set; }
        public string PresionArterialSistolicaConsulta { get; set; }
        public string PresionArterialDiastolicaConsulta { get; set; }
        public string PulsoConsulta { get; set; }
        public string PesoConsulta { get; set; }
        public string TallaConsulta { get; set; }
        public string PlanTratamientoConsulta { get; set; }
        public string ObservacionConsulta { get; set; }
        public string AntecedentesPersonalesConsulta { get; set; }
        public int? DiasIncapacidadConsulta { get; set; }
        public int MedicoConsultaD { get; set; }
        public int EspecialidadId { get; set; }
        public int EstadoConsultaC { get; set; }

        // Parámetros JSON para tablas relacionadas
        public string AlergiasJson { get; set; }
        public string CirugiasJson { get; set; }
        public string MedicamentosJson { get; set; }
        public string LaboratorioJson { get; set; }
        public string ImagenesJson { get; set; }
        public string DiagnosticosJson { get; set; }
        public string OrganosSistemasJson { get; set; }
        public string ExamenFisicoJson { get; set; }
        public string AntecedentesFamiliaresJson { get; set; }


    }
}
