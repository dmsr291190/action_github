using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Reunion
{
    public class SP_OBTENER_REUNION_HISTORIAL_Request_Entity
    {
        public int IdReunionSolicitud { get; set; }
        public string Comentario { get; set; }
        public string FechaRegistra { get; set; }
        public string EstadoReunion { get; set; }
    }
}
