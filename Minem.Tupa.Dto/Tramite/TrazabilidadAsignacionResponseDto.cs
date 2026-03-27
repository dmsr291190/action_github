using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class TrazabilidadAsignacionReponseDto
    {
        public int? IdRow { get; set; }
        public string? DenUniOrganica { get; set; }
        public string? Emisor { get; set; }
        public string? Receptor { get; set; }
        public DateTime FechIniAsignacion { get; set; }
        public DateTime? FechFinAsignacion { get; set; }        
        public int? IdEstadoAsignacion { get; set; }
        public string? DescripcionEstadoAsignacion { get; set; }
        public long? codMovTupaAsignacion { get; set; }

        public string TextIniAsignacion
        {
            get
            {
                if (FechIniAsignacion == DateTime.MinValue) return string.Empty;
                return FechIniAsignacion.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
        public string TextFinAsignacion
        {
            get
            {
                if (!FechFinAsignacion.HasValue) return string.Empty;
                if (FechFinAsignacion == DateTime.MinValue) return string.Empty;
                return FechFinAsignacion.Value.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }



        //*****************************
   
        public int NumeroExpediente { get; set; }
        public int? NumeroExpedientePadre { get; set; }

        public int? IdEtapaSituacionCambio { get; set; }
        public string NombreEtapaSituacionCambio { get; set; }
        public int? CodMaeEstadoSituacionCambio { get; set; }
        public string EstadoDenominacionSituacionCambio { get; set; }
        public int? IdSituacionCambio { get; set; }
        public string DescripcionSituacionCambio { get; set; }
        public int? IdResponsableSituacionCambio { get; set; }
        public string NombreResponsableSituacionCambio { get; set; }
        public int? IdTupaAsignacionSituacion { get; set; }
        //******************************
    }
}
