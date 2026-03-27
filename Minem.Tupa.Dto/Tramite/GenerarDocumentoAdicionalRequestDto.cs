using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class GenerarDocumentoAdicionalRequestDto
    {
        public long IdTramite { get; set; }
        public string NombreArchivo { get; set; }
        public long NumeroDocumentoRespuesta { get; set; }
        public long TipoTramite { get; set; }
        public long TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }
        public string Descripcion { get; set; }        

    }
}
