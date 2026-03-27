using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_OBTENER_BALANCE_Response_Entity
    {
        public long infoBalanceId { get; set; }
        public float periodo { get; set; }
        public float producOil { get; set; }
        public float producGas { get; set; }
        public float producAgua { get; set; }
        public float consumoGas { get; set; }
        public float inyeccionGas { get; set; }
        public float quemaGas { get; set; }
        public int estado { get; set; }
    }
}
