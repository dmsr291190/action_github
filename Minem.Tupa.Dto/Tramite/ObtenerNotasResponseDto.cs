using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Tramite
{
    public class ObtenerNotasResponseDto
    {
        public long Id { get; set; }
        public int TupaId { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
    }
}
