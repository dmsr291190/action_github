using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Profesional
{
    public class SP_OBTENER_PROFESIONES_Response_Entity
    {
        public long ProfesionId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaModif { get; set; }
    }
}
