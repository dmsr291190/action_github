using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tramite
{
    public class SP_INSERT_ARCHIVO_REQUISITO_SOLICITUD_Request_Entity
    {
        public long archivoId { get; set; }
        public long solicitudId { get; set; }
        public long requisitoId { get; set; }
        public string nombreArchivo { get; set; }
        public long usuarioId { get; set; }
    }
}
