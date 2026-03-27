using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Documento
{
    public class DescargaBloqueRequestDto
    {
        public int IdSolicitud { get; set; }
        public List<int>? ListaIdDocumentos { get; set; }
    }
}
