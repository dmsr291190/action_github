using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.AutorizacionQuemaGas
{
    public class AdjuntoInformeDto
    {
        public long infoArchivoId { get; set; }
        public long informeId { get; set; }
        public string nombreArchivo { get; set; }
        public int seccion { get; set; }
        public string fechaRegistra { get; set; }
        public int estado { get; set; }
        public long usuarioId { get; set; }
    }
}
