using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Dto.AutorizacionQuemaGas
{
    public class FacilidadDto
    {
        public string descripcion {  get; set; }
        public int estado { get; set; }
        public long informeId { get; set; }
        public long infoFacilidadId { get; set; }
        public long infoMotivoFacilidadId { get; set; }
        public long infoMotivoId { get; set; }
        public float? gor { get; set; }
        public string? latitud { get; set; }
        public string? longitud { get; set; }
        public long usuarioId { get; set; }
    }
}
