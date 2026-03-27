using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Observacion
{
    public class PRC_I_OBSHISTJSON_Request_Entity
    {
        public int idtumovmetadahistorico { get; set; }
        public int codmaesolicitud { get; set; }
        public string capitulo { get; set; }
        public int estado { get; set; }
        public string usuarioCreacion { get; set; }

    }
}
