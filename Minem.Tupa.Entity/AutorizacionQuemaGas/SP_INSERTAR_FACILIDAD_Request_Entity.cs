using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_INSERTAR_FACILIDAD_Request_Entity
    {
        public long infoFacilidadId { get; set; }
        public long infoMotivoId { get; set; }
        public string descripcion { get; set; }
        public long informeId { get; set; }
        public float? gor { get; set; }
        public string? latitud { get; set; }
        public string? longitud { get; set; }
        public int estado { get;set; }
        public long usuarioId { get; set; }
    }
}
