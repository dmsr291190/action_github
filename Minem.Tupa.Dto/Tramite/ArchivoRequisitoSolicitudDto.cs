using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ArchivoRequisitoSolicitudDto
    {
        public long archivoId { get; set; }
        public long solicitudId { get; set; }
        public long requisitoId { get; set; }
        public int estadoId { get; set; }
        public string nombreArchivo { get; set; }
        public long usuarioId { get; set; }
    }
}
