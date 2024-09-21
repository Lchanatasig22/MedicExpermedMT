using System;
using System.Collections.Generic;

namespace MedicExpermedMT.Models
{
    public partial class Consultum
    {
        public Consultum()
        {
            AntecedentesFamiliares = new HashSet<AntecedentesFamiliare>();
            Cita = new HashSet<Citum>();
            ConsultaAlergia = new HashSet<ConsultaAlergia>();
            ConsultaCirugia = new HashSet<ConsultaCirugia>();
            ConsultaDiagnosticos = new HashSet<ConsultaDiagnostico>();
            ConsultaImagens = new HashSet<ConsultaImagen>();
            ConsultaLaboratorios = new HashSet<ConsultaLaboratorio>();
            ConsultaMedicamentos = new HashSet<ConsultaMedicamento>();
            ExamenFisicos = new HashSet<ExamenFisico>();
            OrganosSistemas = new HashSet<OrganosSistema>();
        }

        public int IdConsulta { get; set; }
        public DateTime? FechacreacionConsulta { get; set; }
        public string? UsuariocreacionConsulta { get; set; }
        public string HistorialConsulta { get; set; } = null!;
        public int? SecuencialConsulta { get; set; }
        public int PacienteConsultaP { get; set; }
        public string? MotivoConsulta { get; set; }
        public string? EnfermedadConsulta { get; set; }
        public string? NombreparienteConsulta { get; set; }
        public string? SignosalarmaConsulta { get; set; }
        public string? Reconofarmacologicas { get; set; }
        public int? TipoparienteConsulta { get; set; }
        public string? TelefonoParienteConsulta { get; set; }
        public string TemperaturaConsulta { get; set; } = null!;
        public string FrecuenciarespiratoriaConsulta { get; set; } = null!;
        public string PresionarterialsistolicaConsulta { get; set; } = null!;
        public string PresionarterialdiastolicaConsulta { get; set; } = null!;
        public string PulsoConsulta { get; set; } = null!;
        public string PesoConsulta { get; set; } = null!;
        public string TallaConsulta { get; set; } = null!;
        public string? PlantratamientoConsulta { get; set; }
        public string? ObservacionConsulta { get; set; }
        public string? AntecedentespersonalesConsulta { get; set; }
        public int? DiasincapacidadConsulta { get; set; }
        public int MedicoConsultaD { get; set; }
        public int EspecialidadId { get; set; }
        public int EstadoConsultaC { get; set; }

        public virtual Especialidad Especialidad { get; set; } = null!;
        public virtual Usuario MedicoConsultaDNavigation { get; set; } = null!;
        public virtual Paciente PacienteConsultaPNavigation { get; set; } = null!;
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliares { get; set; }
        public virtual ICollection<Citum> Cita { get; set; }
        public virtual ICollection<ConsultaAlergia> ConsultaAlergia { get; set; }
        public virtual ICollection<ConsultaCirugia> ConsultaCirugia { get; set; }
        public virtual ICollection<ConsultaDiagnostico> ConsultaDiagnosticos { get; set; }
        public virtual ICollection<ConsultaImagen> ConsultaImagens { get; set; }
        public virtual ICollection<ConsultaLaboratorio> ConsultaLaboratorios { get; set; }
        public virtual ICollection<ConsultaMedicamento> ConsultaMedicamentos { get; set; }
        public virtual ICollection<ExamenFisico> ExamenFisicos { get; set; }
        public virtual ICollection<OrganosSistema> OrganosSistemas { get; set; }
    }
}
