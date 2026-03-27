using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.Observacion
{
    public class ObservacionRequestDto
    {
        public int idobshistjson { get; set; }
        public int codmaesolicitud { get; set; }
        public string capitulo { get; set; }
        public int estado { get; set; }
        public string usuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }
        public string usuarioUltimaMod { get; set; }
        public string fechaUltimaMod { get; set; }
        public int iddetobshistjson { get; set; }
        public string observacion { get; set; }
        public string nomnbre { get; set; }
        public int codmovpersona { get; set; }
        public int iddetobshistjsonpadre { get; set; }
        public int orden { get; set; }
        public int estadoObservacion { get; set; }
    }
}
