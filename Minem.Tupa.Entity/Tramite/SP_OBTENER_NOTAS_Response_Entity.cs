using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tramite
{
    public class SP_OBTENER_NOTAS_Response_Entity
    {
        public long Id { get; set; }
        public int TupaId { get; set; }
        public int Secuencia { get; set; }
        public string Descripcion { get; set; }
    }
}
