using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Tramite
{
    public class USP_S_OBTENER_DOCUMENTOS_ADCIONALES_Response_Entity
    {
        public string descripcion { get; set; }
        public long rutaDocumento { get; set; }
        public string denominacionTramite { get; set; }
        public string demoninacionDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string descripcionTramite { get; set; }
        public long idTramite { get; set; }
        public long expediente { get; set; }


    }
}
