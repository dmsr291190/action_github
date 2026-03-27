using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ObtenerDocumentoAdicionalResponseDto
    {
        public string descripcion { get; set; }
        public long rutaDocumento { get; set; }
        public string denominacionTramite { get; set; }
        public string demoninacionDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaRegistro { get; set; }
        public string descripcionTramite { get; set; }
        public long idTramite { get; set; }
        public long expediente { get; set; }
        public List<Documentos> Documentos { get; set; }
    }

    public class Documentos
    {
        public int TramiteDocId { get; set; }
        public int TramiteId { get; set; }
        public string Descripcion { get; set; }
        public string RutaDocumento { get; set; }
        public string IdAlfresco { get; set; }
    }
}
