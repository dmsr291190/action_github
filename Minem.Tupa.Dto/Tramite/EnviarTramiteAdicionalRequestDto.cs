using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class EnviarTramiteAdicionalRequestDto
    {
        public int IdSolicitud { get; set; }
        public List<DocumentosAdjuntos> Documentos { get; set; }
        public int? NroExpediente { get; set; }
        public int TipoComunicacion { get; set; }

    }
 
}
