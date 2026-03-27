using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Observacion
{
    public class PRC_S_ULTIMOTUMOVMETADAHISTORICO_Response_Entity
    {
        public int idtumovmetadahistorico { get; set; }
        public int id { get; set; }
        public DateTime registro { get; set; }
        public string datajson { get; set; }
        public long codmaesolicitud { get; set; }
        public int version { get; set; }
        public int estado { get; set; }
        public int notificado { get; set; }
        public string descripcionversion { get; set; }
        public string usuarioCreacion { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
