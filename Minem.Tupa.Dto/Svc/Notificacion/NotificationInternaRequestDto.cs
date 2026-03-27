using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Svc.Notificacion
{
    public class NotificationInternaRequestDto
    {
        public string NumDocumento { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public int CodMaeCategoria { get; set; }
        public List<int> notificationAttaches { get; set; }
        public bool SelloTiempo { get; set; }
        public string CodTipoPersona { get; set; }
        public string CodTipoDocumento { get; set; }
        public string NumDocAdicional { get; set; }
        public int IdSolicitud { get; set; }
        public int Expediente { get; set; }
        public int IdSistema { get; set; }
    }
    public class ArchivoAdjuntoNotificacionInterna
    {
        public string FileAdjunto { get; set; }
        public string NomAdjunto { get; set; }
    }
}
