using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ObtenerSolicitudResponseDto
    {
        public string NombreProcedimiento { get; set; }
        public string CodigoProcedimiento { get; set; }
        public string NombreUnidadOrganica { get; set; }
        public string NroComprobante { get; set; }
        public string FechaPago { get; set; }

        public string NombreProyecto { get; set; }
        public string RazonSocialSolicitante { get; set; }
        public string NroDocumentoSolicitante { get; set; }
        public string NroRucSolicitante { get; set; }
        public string Asiento { get; set; }
        public string PartidaRegistral { get; set; }
        public string TelefonoSolicitante { get; set; }
        public string CelularSolicitante { get; set; }
        public string CorreoSolicitante { get; set; }
        public string DomicilioLegalSolicitante { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string RepresentanteLegal { get; set; }
        public string DomicilioRepresentante { get; set; }
        public string DescSolicitadoLinea01 { get; set; }
    }
}
