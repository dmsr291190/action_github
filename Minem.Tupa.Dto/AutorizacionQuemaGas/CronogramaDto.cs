using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.AutorizacionQuemaGas
{
    public class CronogramaDto
    {
        public long infoCronogramaId { get; set; }
        public long infoMotivoFacilidadId { get; set; }
        public long informeId { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public float volGasQuemado { get; set; }
        public float? volLiquidoQuemado { get; set; }
        public int estado { get; set; }
        public long usuarioId { get; set; }
    }
}
