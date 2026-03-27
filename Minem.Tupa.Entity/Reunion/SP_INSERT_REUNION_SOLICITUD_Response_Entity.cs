using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Reunion
{
    public class SP_INSERT_REUNION_SOLICITUD_Response_Entity
    {
        public long idreunionsolicitud { get; set; }
        public long codmaesolicitud { get; set; }
        public string propuesta { get; set; }
        public string consultora { get; set; }

        public string tipoReunion { get; set; }

        //public DateTime? fechareunion { get; set; }
        public string propuestafecha1 { get; set; }
        public string propuestafecha2 { get; set; }

        public long idArchivo{ get; set; }
        public string nombredelarchivo { get; set; }
        public int estado { get; set; }
        public long usuarioregistra { get; set; }
        public DateTime? fechacreacion { get; set; }

        public long usuariomodifica { get; set; }
    }
}
