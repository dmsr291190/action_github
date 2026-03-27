using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Entity.AutorizacionQuemaGas
{
    public class SP_OBTENER_FACILIDAD_Response_Entity
    {
        public long infoMotivoFacilidadId {  get; set; }
        public long infoFacilidadId { get; set; }
        public long infoMotivoId { get; set; }
        public string descripcion {  get; set; }
        public float? gor { get; set; }
        public string? latitud { get; set; }
        public string? longitud { get; set; }
        public int estado { get; set; }
    }
}
