using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class GenerarTramiteRequestDto
    {
        public int TupaId { get; set; }
        public int CodigoEstado { get; set; }
        public int CodigoPersona { get; set; }
    }
}
