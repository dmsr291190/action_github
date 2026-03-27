using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Solicitud
{
    public class SolicitudResponse_Entity
    {
        public string CodMaeTupa { get; set; }
        public string DenominacionEstadoSolicitud { get; set; }
        public string AbreviaturaTupa { get; set; }
        public string NombreTitular { get; set; }
        public string NombreProyecto { get; set; }
        public string UnidadMinera { get; set; }
        public string NumSTD { get; set; }
        public int IdSituacion { get; set; }
        public int IdEtapa { get; set; }
    }
}
