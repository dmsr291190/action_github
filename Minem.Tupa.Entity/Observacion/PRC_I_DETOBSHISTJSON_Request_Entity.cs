using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Observacion
{
    public class PRC_I_DETOBSHISTJSON_Request_Entity
    {
        public int iddetobshistjson { get; set; }
        public int idobshistjson { get; set; }
        public int codmovpersona { get; set; }
        public string observacion { get; set; }
        public int iddetobshistjsonpadre { get; set; }
        public int orden { get; set; }
        public int estado { get; set; }
        public string usuarioCreacion { get; set; }

        public int estadoObservacion { get; set; }
    }
}
