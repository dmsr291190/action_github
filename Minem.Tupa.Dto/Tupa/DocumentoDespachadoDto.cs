using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tupa
{
    public class DocumentoDespachadoDto
    {
        public string NroDocumento { get; set; }
        public long IdArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public string Fecha {  get; set; }
    }
}
