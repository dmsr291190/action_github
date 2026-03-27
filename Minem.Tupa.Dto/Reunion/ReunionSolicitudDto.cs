using System;
using Minem.Tupa.Dto.Reunion;

namespace Minem.Tupa.Dto.Reunion
{
    public class ReunionSolicitudDto
    {
        public long idreunionsolicitud { get; set; }
        public long codmaesolicitud { get; set; }
        public string propuesta { get; set; }
        public string consultora { get; set; }

        public string tipoReunion { get; set; }

        //public DateTime? fechareunion { get; set; }
        public string propuestafecha1 { get; set; }
        public string propuestafecha2 { get; set; }

        public long idArchivo { get; set; }
        public string nombredelarchivo { get; set; }
        public int estado { get; set; }
        public long usuarioregistra { get; set; }
        public DateTime? fechacreacion { get; set; }

        public long usuariomodifica { get; set; }

        public List<ReunionParticipanteDto> representantesTitular { get; set; }

        public List<ReunionParticipanteDto> representantesConsultora { get; set; }

        public List<string> correos { get; set; }
        public List<ReunionObjetivoDto> objetivos { get; set; }

    }
}
