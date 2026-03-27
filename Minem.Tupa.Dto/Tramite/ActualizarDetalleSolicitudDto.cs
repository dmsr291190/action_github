using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ActualizarDetalleSolicitudDto
    {
        public long solicitudId { get; set; }
        public long requisitoId { get; set; }
        public int estadoId { get; set; }
    }
}
