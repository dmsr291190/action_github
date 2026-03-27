using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tupa
{
    public class ObtenerTupaPorSectorRequest
    {
        public long idSector {  get; set; }
        public required string tipoPersona {  get; set; }
    }
}
