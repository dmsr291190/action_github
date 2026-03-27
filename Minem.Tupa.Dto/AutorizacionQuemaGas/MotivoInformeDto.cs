using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.AutorizacionQuemaGas
{
    public class MotivoInformeDto
    {
        public long infoMotivoId { get; set; }
        public long motivoId { get; set; }
        public long? motivoPadreId { get; set; }
        public long informeId { get; set; }
        public string descripcion { get; set; }
        public string? informacionAdicional { get; set; }
        public long usuarioId { get; set; }
    }
}
