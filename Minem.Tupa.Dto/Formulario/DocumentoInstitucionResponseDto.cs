using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Formulario
{
    public class DocumentoInstitucionResponseDto
    {
        public int IdSolicitudOpinanteRespuesta { get; set; }
        public int IdSolicitudOpinante { get; set; }
        public string NroExpediente { get; set; }
        public DateTime? FechaExpediente { get; set; }
        public int IdAsuntoAdjunto { get; set; }
        public string NroOficioNotificado { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public string NroCut { get; set; }
        public string CodigoExpedientePrincipal { get; set; }
        public string Ruc { get; set; }
        public string UsuarioExterno { get; set; }
        public int IdTipoDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public string Asunto { get; set; }
        public string CodigoExpedienteAnexado { get; set; }
        public int CodigoResultado { get; set; }
        public string EnvioCorreo { get; set; }
        public int Estado { get; set; }
        public decimal UsuarioRegistra { get; set; }
        public DateTime FechaRegistra { get; set; }
        public decimal UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        //***
        public string AsuntoAdjunto { get; set; }
        public int NotificarImpulso { get; set; }
    }
}
