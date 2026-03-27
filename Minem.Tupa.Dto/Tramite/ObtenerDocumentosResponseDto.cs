using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ObtenerDocumentosResponseDto
    {
        public int TramiteDocId { get; set; }
        public int TramiteId { get; set; }
        public string Descripcion { get; set; }
        public string RutaDocumento { get; set; }
        public string IdAlfresco { get; set; }
    }
}
