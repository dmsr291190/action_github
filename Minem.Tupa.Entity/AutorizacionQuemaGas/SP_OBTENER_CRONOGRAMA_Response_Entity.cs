using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_OBTENER_CRONOGRAMA_Response_Entity
    {
        public long infoCronogramaId { get; set; }
        public long infoMotivoFacilidadId { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public float volGasQuemado { get; set; }
        public float? volLiquidoQuemado { get; set; }
        public int estado { get; set; }
    }
}
