using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tramite
{
    public class SP_OBTENER_DATOS_DETALLADO_SOLICITUD_Response_Entity
    {
        public int CodMaeSolicitud { get; set; }
        public int CodMovPersona { get; set; }
        public string CodMaeTupa { get; set; }
        public int CodMaeEstado { get; set; }
        public int CodMaecatRequisito { get; set; }
        public string NumDocumento { get; set; }
        public string Descripcion { get; set; }
        public int CodIdMaeTupa { get; set; }
        public string NombreProyecto { get; set; }
        public string Objetivo { get; set; }
        public string NombreTitular { get; set; }
        public string Direccion { get; set; }
        public string Region { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Ruc { get; set; }
        public string Email { get; set; }
        public string PaginaWeb { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string EmailNotificacion1 { get; set; }
        public string EmailNotificacion2 { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }

        public string NombreProcedimiento { get; set; }
        public string AbreviaturaDependencia { get; set; }
        public string DenominacionDependencia { get; set; }
        public string Departamento{ get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string NumPartida { get; set; }
        public string NumAsiento { get; set; }
        public string DomicilioRepresentante { get; set; }
        public string NroDocumentoSolicitante { get; set; }
        public string RepresentanteLegal { get; set; }

        public string NroComprobante { get; set; }
        public string FechaPago { get; set; }
        public string DescSolicitadoLinea01 { get; set; }
    }
}
