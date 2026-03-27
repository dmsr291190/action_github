using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.AutorizacionQuemaGas
{
    public class MotivoDto
    {
        public long motivoId { get; set; }
        public string nombre { get; set; }
        public int clase { get; set; }
        public long? motivoPadreId { get; set; }
    }
}
