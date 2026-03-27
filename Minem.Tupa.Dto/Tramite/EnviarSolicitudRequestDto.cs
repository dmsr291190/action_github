using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class EnviarSolicitudRequestDto
    {
        public int IdSolicitud {  get; set; }
        public List<DocumentosAdjuntos> Documentos {  get; set; }
        public int? NroExpediente {  get; set; }
        public int IdEstadoTramiteAdmisibilidad { get; set; }

        //####################################################        
        public int CodMaeEstado { get; set; }
        public int CodSituacion { get; set; }
        public int CodEtapa { get; set; }
    }

    public class DocumentosAdjuntos
    {
        public int CodigoGenerado { get; set; }
        public int IdRequisito { get; set; }
        public string NombreDocumento {  get; set; }
    }
}
