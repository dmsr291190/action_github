using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_INSERTAR_ACCION_Request_Entity
    {
        public long infoAccionId { get; set; }
        public long informeId { get; set; }
        public string descripcion { get; set; }
        public int esObjetivo { get; set; }
        public float estado { get; set; }
        public long usuarioId { get; set; }
    }
}
