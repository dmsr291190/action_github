using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_INSERTAR_ADJUNTO_INFORME_Request_Entity
    {
        public long infoArchivoId { get; set; }
        public long informeId { get; set; }
        public string nombreArchivo { get; set; }
        public int seccion { get; set; }
        public int estado { get; set; }
        public long usuarioId { get; set; }
    }
}
