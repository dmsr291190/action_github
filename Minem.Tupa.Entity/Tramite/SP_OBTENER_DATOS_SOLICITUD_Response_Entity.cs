using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tramite
{
    public class SP_OBTENER_DATOS_SOLICITUD_Response_Entity
    {
        public int CodMaeSolicitud { get; set; }
        public int CodMovPersona { get; set; }
        public string CodMaeTupa { get; set; }
        public int CodMaeEstado { get; set; }
        public int CodMaecatRequisito { get; set; }
        public string NumDocumento { get; set; }
        public string Descripcion { get; set; }
        public int CodIdMaeTupa { get; set; }
        public string NombreProyecto { get; set; }
        public long? CodMaeSolicitudOrigen { get; set; }

    }
}
