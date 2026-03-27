using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.AutorizacionQuemaGas
{
    public class BuscarQuemadorRequestDto
    {
        public long usuarioId { get; set; }
        public string serie { get; set; }
        public string nombre { get; set; }
    }
}
