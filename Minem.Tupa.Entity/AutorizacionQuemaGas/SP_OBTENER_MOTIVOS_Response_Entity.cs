using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_OBTENER_MOTIVOS_Response_Entity
    {
        public long motivoId {  get; set; }
        public string nombre { get; set; }
        public int clase {  get; set; }
        public long? motivoPadreId { get; set; }
    }
}
