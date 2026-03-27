using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.Its
{
    public class USP_S_OBTENER_REUNION_ADJUNTO_Response_Entity
    {
        public int ID_REUNION_ADJUNTO { get; set; }
        public int ID_REUNION_SOLICITUD { get; set; }
        public int ID_ARCHIVO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public int ESTADO { get; set; }
        public int USUARIO_REGISTRA { get; set; }
        public DateTime FECHA_REGISTRA { get; set; }
        public int USUARIO_MODIFICA { get; set; }
        public DateTime? FECHA_MODIFICA { get; set; }
    }
}
