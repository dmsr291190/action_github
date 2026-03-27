using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Formulario
{
    public class TransactionResumeData
    {
        public int CodMaeSolicitud { get; set; }
        public string NumSTD { get; set; }
        public string FechRegSolicitud { get; set; }
        public string RazonSocial { get; set; }
        public string TipoPersona { get; set; }
        public string Documento { get; set; }
        public string NomCompleto { get; set; }
        public string TipoDiasAtencion { get; set; }
        public string FechIniSolicitud { get; set; }
        public string FechFinSolicitud { get; set; }
        public string NumDiaTranscurrido { get; set; }
        public string DiasAtencion { get; set; }
        public string CodTabTipoPersona { get; set; }
        public int CodIdMaeTupa { get; set; }
        public string CodMaeTupa { get; set; }
        public string Denominacion { get; set; }
        public string ClaveSTD { get; set; }
        public long CodMovPersona { get; set; }
        public int CodMaeEstado { get; set; }
        public string DenEstadoSolicitud { get; set; }
        public string NombreSituacion { get; set; }
        public int idSituacion { get; set; }
        public int idEtapa { get; set; }
        public string NombreProyecto { get; set; }
        public string UnidadMinera { get; set; }
        public int IdSolicitudExpediente { get; set; }
        public int NumeroExpediente { get; set; }
        public string NomcompletoEvaluadorPrincipal { get; set; }
        public string NomcompletoEvaluadorAlterno { get; set; }
        public string Ubicacion { get; set; }
        public string NombreTitular { get; set; }
        public string RucTitular { get; set; }
        public string Inversion { get; set; }
        public string SolicitaTitulo { get; set; }
        public string FechaImpulso { get; set; }
        public bool TieneImpulso { get; set; }
        public DateTime? FechaFinSubsanacion { get; set; }
        public DateTime? FechaFinApertura { get; set; }
    }
}
