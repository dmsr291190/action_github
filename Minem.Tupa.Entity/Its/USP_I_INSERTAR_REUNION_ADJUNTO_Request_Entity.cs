using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Its
{
    public class USP_I_INSERTAR_REUNION_ADJUNTO_Request_Entity
    {
        public int IdReunionSolicitud { get; set; }
        public string NombreArchivo { get; set; }
        public int IdArchivo { get; set; }
        public int Usuario { get; set; }
    }
}
